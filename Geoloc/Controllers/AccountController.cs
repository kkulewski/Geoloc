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

namespace Geoloc.Controllers
{
    [Route("api/[controller]")]
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

        // POST api/account
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
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
        
        [HttpGet]
        public IActionResult Index()
        {
            var token = new JwtTokenFactory(_configuration, "MemberToken")
                .AddClaim("MembershipId", "123")
                .Build();

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpGet("[action]")]
        [Authorize(Policy = "Member")]
        public IActionResult Claims()
        {
            return Ok(HttpContext.User.Claims.ToDictionary(c => c.Type, c => c.Value));
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
                return BadRequest(ModelState.TryAddModelError("login_failure", "Invalid username or password."));

            var token = new JwtTokenFactory(_configuration, credentials.UserName)
                .AddClaim(identity.FindFirst("rol"))
                .AddClaim(identity.FindFirst("id"))
                .Build();

            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return new OkObjectResult(response);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);
                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(GenerateClaimsIdentity(userName, userToVerify.Id));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        private ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim("id", id),
                new Claim("rol", "api_access")
            });
        }
    }
}
