using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using CarShopAPI.Services.Search;

namespace CarShopAPI.Filters
{
    public class ModelFilterStrategy : IFilterStrategy
    {
        private readonly string? _model;
        public ModelFilterStrategy(string? model)
        {
            _model = model;
        }

        public bool CanApply(CarFilter filter)
        {
            return !string.IsNullOrEmpty(filter.Model);
        }

        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.Model.ToLower().Contains(_model.ToLower()));
        }
    }
}