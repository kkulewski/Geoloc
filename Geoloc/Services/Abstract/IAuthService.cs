using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Geoloc.Models.WebModels;
using Microsoft.AspNetCore.Identity;

namespace Geoloc.Services.Abstract
{
    public interface IAuthService
    {
        Task<string> GetUserNameById(string userId);
        Task<IdentityResult> Register(RegisterWebModel model);
        Task<JwtSecurityToken> CreateToken(LoginWebModel model);
    }
}
