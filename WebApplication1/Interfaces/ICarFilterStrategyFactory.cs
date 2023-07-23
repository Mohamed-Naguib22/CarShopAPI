using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface ICarFilterStrategyFactory
    {
        IEnumerable<ICarFilterStrategy> GetFilterStrategies(CarFilter filter);
    }
}
