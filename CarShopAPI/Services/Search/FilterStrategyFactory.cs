using CarShopAPI.Filters;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Services
{
    public class FilterStrategyFactory : IFilterStrategyFactory
    {
        private readonly IEnumerable<IFilterStrategy> _filterStrategies;
        public FilterStrategyFactory(IEnumerable<IFilterStrategy> filterStrategies)
        {
            _filterStrategies = filterStrategies;
        }
        public IEnumerable<IFilterStrategy> GetFilterStrategies(CarFilter filter)
        {
            var strategies = new List<IFilterStrategy>
            {
                new YearFilterStrategy(filter.Year),
                new PriceFilterStrategy(filter.Price),
                new IsNewFilterStrategy(filter.IsNew),
                new ModelFilterStrategy(filter.Model),
                new BodyTypeFilterStrategy(filter.BodyType),
                new StateFilterStrategy(filter.State),
                new ManufacturerFilterStrategy(filter.Manufacturer)
            };
            return strategies.Where(strategy => strategy.CanApply(filter));
        }
    }
}