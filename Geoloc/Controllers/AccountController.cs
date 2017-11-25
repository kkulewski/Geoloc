using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Models.Entities;
using Geoloc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Geoloc.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;

        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ApplicationDbContext appDbContext, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // GET api/account/addclaim
        [HttpGet]
        public IActionResult AddClaim()
        {
            var token = new JwtTokenFactory(_configuration, "MemberToken")
                .AddClaim("MembershipId", "123")
                .Build();

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        // GET api/account/checkclaim
        [HttpGet]
        public IActionResult CheckClaim()
        {
            return Ok(HttpContext.User.Claims.ToDictionary(c => c.Type, c => c.Value));
        }

        // POST api/account/register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.TryAddModelError(e.Code, e.Description);

                return new BadRequestObjectResult(ModelState);
            }

            await _appDbContext.SaveChangesAsync();
            return new OkObjectResult("Account created");
        }

        // POST api/account/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                ModelState.TryAddModelError("login_failure", "Invalid username or password.");
                return new BadRequestObjectResult(ModelState);
            }

            var token = new JwtTokenFactory(_configuration, credentials.UserName)
                .AddClaim(identity.FindFirst("rol"))
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
                return null;
            
            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null)
                return null;
            
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
                return await Task.FromResult(GenerateClaimsIdentity(userName, userToVerify.Id));

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        private ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            var identity = new GenericIdentity(userName, "Token");
            var claims = new[]
            {
                new Claim("id", id),
                new Claim("rol", "api_access")
            };

            return new ClaimsIdentity(identity, claims);
        }
    }
}
