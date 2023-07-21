using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using CarShopAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IEntityService<State> _stateService;
        public LocationController(IEntityService<State> stateService)
        {
            _stateService = stateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var states = _stateService.GetAll();

            return Ok(states);
        }
    }
}

