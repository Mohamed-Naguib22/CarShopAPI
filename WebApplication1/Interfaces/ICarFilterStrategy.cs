using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface ICarFilterStrategy
    {
        IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query);
    }
}
