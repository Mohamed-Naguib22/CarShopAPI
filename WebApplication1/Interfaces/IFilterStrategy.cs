using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IFilterStrategy
    {
        IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query);
    }
}