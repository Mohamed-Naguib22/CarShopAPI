using CarShopAPI.Data;
using CarShopAPI.Extensions;
using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Filter([FromBody] CarFilter filter)
        {
            var filteredCars = await _searchService.Filter(filter);
            return Ok(filteredCars);
        }
    }
}
