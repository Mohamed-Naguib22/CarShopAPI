using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BodyTypeController : ControllerBase
    {
        private readonly IEntityService<BodyType> _bodyTypeService;
        public BodyTypeController(IEntityService<BodyType> bodyTypeService)
        {
            _bodyTypeService = bodyTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bodyTypes = _bodyTypeService.GetAll();

            return Ok(bodyTypes);
        }
    }
}
