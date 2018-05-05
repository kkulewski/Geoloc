using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Geoloc.Models.WebModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Geoloc.Controllers
{
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/account/username
        [HttpPost]
        public async Task<IActionResult> Username([FromBody] string id)
        {
            var username = await _authService.GetUserNameById(id);
            return new OkObjectResult(JsonConvert.SerializeObject(username));
        }

        // POST api/account/register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterWebModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.Register(model);
            foreach (var e in result.Errors)
            {
                ModelState.TryAddModelError(e.Code, e.Description);
            }

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return new OkObjectResult(JsonConvert.SerializeObject("Account created"));
        }

        // POST api/account/login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginWebModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authService.CreateToken(model);
            if (token == null)
            {
                ModelState.TryAddModelError("login_failure", "Invalid username or password.");
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ModelState));
            }

            var response = new
            {
                id = token.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value,
                auth_token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }
    }
}
