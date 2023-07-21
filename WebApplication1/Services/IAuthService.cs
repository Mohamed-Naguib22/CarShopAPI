using CarShopAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace CarShopAPI.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAysnc(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenrRequestModel model);
    }
}
