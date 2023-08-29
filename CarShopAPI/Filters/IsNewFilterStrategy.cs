using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using CarShopAPI.Services.Search;

namespace CarShopAPI.Filters
{
    public class IsNewFilterStrategy : IFilterStrategy
    {
        private readonly bool? _isNew;
        public IsNewFilterStrategy(bool? isNew)
        {
            _isNew = isNew;
        }
        public bool CanApply(CarFilter filter)
        {
            return filter.IsNew.HasValue;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.IsNew == _isNew);
        }
    }
}