using CarShopAPI.Filters;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Services
{
    public class FilterStrategyFactory : IFilterStrategyFactory
    {
        public IEnumerable<IFilterStrategy> GetFilterStrategies(CarFilter filter)
        {
            return new List<IFilterStrategy>
            {
                filter.Year.HasValue ? new YearFilterStrategy(filter.Year.Value) : null,
                filter.Price.HasValue ? new PriceFilterStrategy(filter.Price.Value) : null,
                filter.IsNew.HasValue ? new IsNewFilterStrategy(filter.IsNew.Value) : null,
                string.IsNullOrEmpty(filter.Model) ? null : new ModelFilterStrategy(filter.Model),
                string.IsNullOrEmpty(filter.BodyType) ? null : new BodyTypeFilterStrategy(filter.BodyType),
                string.IsNullOrEmpty(filter.State) ? null : new StateFilterStrategy(filter.State),
                string.IsNullOrEmpty(filter.Manufacturer) ? null : new ManufacturerFilterStrategy(filter.Manufacturer)
            }
            .Where(strategy => strategy != null);
        }
    }
}