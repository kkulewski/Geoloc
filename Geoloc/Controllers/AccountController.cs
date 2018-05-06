using System;
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
        private readonly IUserService _userService;

        public AccountController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        // POST api/account/username
        [HttpPost]
        public IActionResult Username([FromBody] Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }

            return new OkObjectResult(JsonConvert.SerializeObject(user.UserName));
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
