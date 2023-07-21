using CarShopAPI.Data;
using CarShopAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public YearController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var years = await _dbContext.Cars.Select(x => x.Year)
                .Distinct().ToListAsync();
            
            return Ok(years);
        }
    }
}
