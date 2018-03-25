using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Models.WebModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Geoloc.Data.Entities;
using Geoloc.Services.Jwt;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ApplicationDbContext appDbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        // POST api/account/username
        [HttpPost]
        public async Task<IActionResult> Username([FromBody]string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            var username = result.UserName;

            return new OkObjectResult(JsonConvert.SerializeObject(username));
        }

        // POST api/account/register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterWebModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = Mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                {
                    ModelState.TryAddModelError(e.Code, e.Description);
                }

                return new BadRequestObjectResult(ModelState);
            }

            await _appDbContext.SaveChangesAsync();
            return new OkObjectResult(JsonConvert.SerializeObject("Account created"));
        }

        // POST api/account/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginWebModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                ModelState.TryAddModelError("login_failure", "Invalid username or password.");
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ModelState));
            }

            var token = new JwtTokenFactory(_configuration, credentials.UserName)
                .AddClaim(identity.FindFirst("role"))
                .AddClaim(identity.FindFirst("id"))
                .Build();

            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            
            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null)
            {
                return null;
            }
            
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        private ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            var identity = new GenericIdentity(userName, "Token");
            var claims = new[]
            {
                new Claim("id", id),
                new Claim("role", "api_access")
            };

            return new ClaimsIdentity(identity, claims);
        }
    }
}
