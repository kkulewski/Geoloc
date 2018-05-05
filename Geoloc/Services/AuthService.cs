using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Geoloc.Services.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Geoloc.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GetUserNameById(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            var username = result.UserName;
            return username;
        }
        
        public async Task<IdentityResult> Register(RegisterWebModel model)
        {
            var user = Mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _unitOfWork.Save();
            }

            return result;
        }

        public async Task<JwtSecurityToken> CreateToken(LoginWebModel model)
        {
            var user = await GetUser(model);
            if (user == null)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim("some_type", "some_value")
            };

            var token = new JwtTokenFactory(_configuration)
                .AddClaims(claims)
                .Build();

            return token;
        }

        private async Task<AppUser> GetUser(LoginWebModel model)
        {
            var invalidModel = string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password);
            if (invalidModel)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return null;
            }

            return user;
        }
    }
}
