using CarShopAPI.Data;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IImageService<ApplicationUser> _userImageService;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserController(IImageService<ApplicationUser> userImageService,
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _userImageService = userImageService;
        }
        [HttpPost]
        public async Task<IActionResult> SetImage([FromForm] ApplicationUser user, string userId)
        {
            _userImageService.SetImage(user);
            await _dbContext.SaveChangesAsync();
            return Ok(new { Message = "Image has been set successsfully"});
        }
    }
}
