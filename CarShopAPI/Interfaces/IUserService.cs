using CarShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CarShopAPI.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> UpdateUserAsync(string userId, JsonPatchDocument<ApplicationUser> patchDocument);
        Task<ApplicationUser> SetImageAsync(IFormFile? imgFile, string userId);
    }
}
