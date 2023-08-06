using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using CarShopAPI.Services.Search;
using Newtonsoft.Json.Linq;

namespace CarShopAPI.Filters
{
    public class BodyTypeFilterStrategy : IFilterStrategy
    {
        private readonly string _bodyType;
        public BodyTypeFilterStrategy(string bodyType)
        {
            _bodyType = bodyType;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.BodyType.ToLower().Contains(_bodyType.ToLower()));
        }
    }
}