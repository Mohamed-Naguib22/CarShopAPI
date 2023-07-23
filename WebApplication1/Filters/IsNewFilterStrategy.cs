using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Filters
{
    public class IsNewFilterStrategy : ICarFilterStrategy
    {
        private readonly bool _isNew;
        public IsNewFilterStrategy(bool isNew)
        {
            _isNew = isNew;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.IsNew == _isNew);
        }
    }
}
