using CarShopAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace CarShopAPI.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAysnc(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenrRequestModel model);
        Task<string> AddRoleAysnc(AddRoleModel model);
        Task<AuthModel> VerifyAsync(TokenModel tokenModel);
        Task<bool> EmailExistsAsyn(string email);
    }
}
