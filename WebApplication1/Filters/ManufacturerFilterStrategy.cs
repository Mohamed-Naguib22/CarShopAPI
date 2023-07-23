using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Filters
{
    public class ManufacturerFilterStrategy : ICarFilterStrategy
    {
        private readonly string _manufacturer;
        public ManufacturerFilterStrategy(string manufacturer)
        {
            _manufacturer = manufacturer;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.Manufacturer.ToLower().Contains(_manufacturer.ToLower()));
        }
    }
}
