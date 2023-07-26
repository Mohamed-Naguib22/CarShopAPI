using CarShopAPI.Data;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public ApplicationUserController(IImageService<ApplicationUser> userImageService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _userImageService = userImageService;
            _dbContext = dbContext;
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> SetImage([FromForm] IFormFile? imgFile, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _userImageService.SetImage(user, imgFile);
            _dbContext.SaveChanges();
            return Ok(new { Message = "Image has been set successsfully" });
        }
    }
}
