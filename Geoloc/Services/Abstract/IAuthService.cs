using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Geoloc.Models.WebModels;
using Microsoft.AspNetCore.Identity;

namespace Geoloc.Services.Abstract
{
    public interface IAuthService
    {
        Task<string> GetUserNameById(string userId);

        Task<IdentityResult> Register(RegisterWebModel model);

        Task<ClaimsIdentity> GetClaimsIdentity(LoginWebModel model);

        Task<JwtSecurityToken> GetUserToken(LoginWebModel model);
    }
}
