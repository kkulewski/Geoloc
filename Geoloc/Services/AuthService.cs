using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Geoloc.Data;
using Geoloc.Data.Entities;
using Geoloc.Models.WebModels;
using Geoloc.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Geoloc.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenFactory _tokenFactory;

        public AuthService(IUnitOfWork unitOfWork, UserManager<User> userManager, JwtTokenFactory tokenFactory)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenFactory = tokenFactory;
        }
        
        public async Task<IdentityResult> Register(RegisterWebModel model)
        {
            var user = Mapper.Map<User>(model);
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

            return _tokenFactory
                .AddClaims(claims)
                .Build();
        }

        private async Task<User> GetUser(LoginWebModel model)
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
