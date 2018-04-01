using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
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

        public async Task<JwtSecurityToken> GetUserToken(LoginWebModel model)
        {
            var identity = await GetClaimsIdentity(model);
            if (identity == null)
            {
                return null;
            }

            var token = new JwtTokenFactory(_configuration, model.UserName)
                .AddClaim(identity.FindFirst("role"))
                .AddClaim(identity.FindFirst("id"))
                .Build();

            return await Task.Run(() => token);
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity(LoginWebModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return null;
            }

            var userToVerify = await _userManager.FindByNameAsync(model.UserName);
            if (userToVerify == null)
            {
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(userToVerify, model.Password);
            if (!passwordValid)
            {
                return null;
            }

            return await Task.FromResult(GenerateClaimsIdentity(model.UserName, userToVerify.Id.ToString()));
        }

        private static ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
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
