using CarShopAPI.Data;
using CarShopAPI.Extensions;
using CarShopAPI.Helpers;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly ICarService _carService;
        private readonly ICarFilterStrategyFactory _filterStrategyFactory;

        public SearchService(ICarService carService, ICarFilterStrategyFactory filterStrategyFactory)
        {
            _carService = carService;
            _filterStrategyFactory = filterStrategyFactory;
        }
        public async Task<IEnumerable<CarDto>> Filter(CarFilter filter, int pageNumber)
        {
            var query = _carService.GetCarsWithRelatedEntities()
                .Select(car => CarMapper.MapCarToDto(car));

            if (filter is not null)
            {
                var filterStrategies = _filterStrategyFactory.GetFilterStrategies(filter);
                foreach (var strategy in filterStrategies)
                {
                    query = strategy.ApplyFilter(query);
                }
            }

            var filteredCars = await query.ToListAsync();
            return filteredCars.Pagenate(pageNumber, 3);
        }
    }
}
