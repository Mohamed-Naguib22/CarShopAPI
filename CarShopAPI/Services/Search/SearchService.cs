using CarShopAPI.Data;
using CarShopAPI.Extensions;
using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services.Search
{
    public class SearchService : BaseService, ISearchService
    {
        private readonly IFilterStrategyFactory _filterStrategyFactory;

        public SearchService(ApplicationDbContext dbContext, IFilterStrategyFactory filterStrategyFactory) : base(dbContext)
        {
            _filterStrategyFactory = filterStrategyFactory;
        }
        public async Task<IEnumerable<CarDto>> Filter(CarFilter filter, int pageNumber)
        {
                var query = MapCarToDto();

            if (filter is not null)
            {
                var filterStrategies = _filterStrategyFactory.GetFilterStrategies(filter);
                foreach (var strategy in filterStrategies)
                {
                    query = strategy.ApplyFilter(query);
                }
            }

            var filteredCars = await query.ToListAsync();
            return filteredCars.Paginate(pageNumber, 10);
        }
    }
}