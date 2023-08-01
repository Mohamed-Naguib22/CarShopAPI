using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using CarShopAPI.Services.Search;

namespace CarShopAPI.Filters
{
    public class YearFilterStrategy : IFilterStrategy
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