using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearController : ControllerBase
    {
        private readonly IYearService _yearService;
        public YearController(IYearService yearService)
        {
            _yearService = yearService;
        }
        [HttpGet]
        public async Task<IActionResult> GetYearsAsync()
        {
            var years = await _yearService.GetYears().ToListAsync();
            return Ok(years);
        }
    }
}
