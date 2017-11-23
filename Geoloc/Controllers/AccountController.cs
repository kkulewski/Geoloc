using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Models.Entities;
using Geoloc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Geoloc.Services.Jwt;
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
            var token = new JwtTokenBuilder()
                .UseDefaultConfiguration(_configuration)
                .AddSubject("Login token")
                .AddClaim("MembershipId", "111")
                .Build();

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
