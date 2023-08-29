using CarShopAPI.Data;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.XPath;

namespace CarShopAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IImageService<ApplicationUser> _userImageService;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public ApplicationUserController(IImageService<ApplicationUser> userImageService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext, IUserService userService)
        {
            _userManager = userManager;
            _userImageService = userImageService;
            _dbContext = dbContext;
            _userService = userService;
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> SetImageAsync([FromForm] IFormFile? imgFile, string userId)
        {
            var user = await _userService.SetImageAsync(imgFile, userId);

            if (!string.IsNullOrEmpty(user.Message))
                return BadRequest(user.Message);

            return Ok(user);
        }
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(string userId, [FromBody] JsonPatchDocument<ApplicationUser> patchDocument)
        {
            var user = await _userService.UpdateUserAsync(userId, patchDocument);

           if(!string.IsNullOrEmpty(user.Message))
                return BadRequest(user.Message);

            return Ok(user);
        }
    }
}
