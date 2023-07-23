using CarShopAPI.Filters;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Services
{
    public class CarFilterStrategyFactory : ICarFilterStrategyFactory
    {
        public IEnumerable<ICarFilterStrategy> GetFilterStrategies(CarFilter filter)
        {
            var strategies = new List<ICarFilterStrategy>();

            if (filter.Year.HasValue)
            {
                strategies.Add(new YearFilterStrategy(filter.Year.Value));
            }

            if (filter.Price.HasValue)
            {
                strategies.Add(new PriceFilterStrategy(filter.Price.Value));
            }

            if (filter.IsNew.HasValue)
            {
                strategies.Add(new IsNewFilterStrategy(filter.IsNew.Value));
            }

            if (!string.IsNullOrEmpty(filter.Model))
            {
                strategies.Add(new ModelFilterStrategy(filter.Model));
            }

            if (!string.IsNullOrEmpty(filter.BodyType))
            {
                strategies.Add(new BodyTypeFilterStrategy(filter.BodyType));
            }

            if (!string.IsNullOrEmpty(filter.State))
            {
                strategies.Add(new StateFilterStrategy(filter.State));
            }

            if (!string.IsNullOrEmpty(filter.Manufacturer))
            {
                strategies.Add(new ManufacturerFilterStrategy(filter.Manufacturer));
            }

            return strategies;
        }
    }
}
