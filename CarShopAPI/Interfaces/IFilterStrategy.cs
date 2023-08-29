using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IFilterStrategy
    {
        bool CanApply(CarFilter filter);
        IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query);
    }
}