using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace CarShopAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> UpdateUserAsync(string userId, JsonPatchDocument<ApplicationUser> patchDocument)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return new ApplicationUser { Message = "User is not found" };

            patchDocument.ApplyTo(user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                user.Message = "Something went wrong";
                return user;
            }

            return user;
        }
    }
}
