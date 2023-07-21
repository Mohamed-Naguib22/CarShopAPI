using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IEntityService<Manufacturer> _manufacturerService;
        public ManufacturerController(IEntityService<Manufacturer> manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var manufacturers = _manufacturerService.GetAll();

            return Ok(manufacturers);
        }
    }
}

