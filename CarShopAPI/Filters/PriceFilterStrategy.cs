using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using CarShopAPI.Services.Search;

namespace CarShopAPI.Filters
{
    public class PriceFilterStrategy : IFilterStrategy
    {
        private readonly decimal _price;
        public PriceFilterStrategy(decimal price)
        {
            _price = price;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.Price <= _price);
        }
    }
}