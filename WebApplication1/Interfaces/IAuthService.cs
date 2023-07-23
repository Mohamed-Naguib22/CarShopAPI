using CarShopAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace CarShopAPI.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAysnc(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenrRequestModel model);
    }
}
