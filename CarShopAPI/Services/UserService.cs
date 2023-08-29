using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace CarShopAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IImageService<ApplicationUser> _userImageService;
        public UserService(UserManager<ApplicationUser> userManager, IImageService<ApplicationUser> userImageService)
        {
            _userManager = userManager;
            _userImageService = userImageService;
        }
        public async Task<ApplicationUser> SetImageAsync(IFormFile? imgFile, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return new ApplicationUser { Message = "User is not found" };

            _userImageService.SetImage(user, imgFile);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                user.Message = "Something went wrong";
                return user;
            }

            return user;
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
