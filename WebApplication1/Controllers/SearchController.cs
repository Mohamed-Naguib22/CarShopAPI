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

        [HttpGet("{pageNumber}")]
        public async Task<IActionResult> Filter([FromBody] CarFilter filter, int pageNumber)
        {
            var filteredCars = await _searchService.Filter(filter, pageNumber);
            return Ok(filteredCars);
        }
    }
}
