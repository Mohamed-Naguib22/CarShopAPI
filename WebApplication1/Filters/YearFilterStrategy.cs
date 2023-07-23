using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Filters
{
    public class YearFilterStrategy : ICarFilterStrategy
    {
        private readonly int _year;
        public YearFilterStrategy(int year)
        {
            _year = year;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.Year == _year);
        }
    }
}
