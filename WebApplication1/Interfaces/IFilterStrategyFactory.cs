using CarShopAPI.Filters;
using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IFilterStrategyFactory
    {
        IEnumerable<IFilterStrategy> GetFilterStrategies(CarFilter filter);
    }
}