using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Filters
{
    public class ModelFilterStrategy : ICarFilterStrategy
    {
        private readonly string _model;
        public ModelFilterStrategy(string model)
        {
            _model = model;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.Model.ToLower().Contains(_model.ToLower()));
        }
    }
}
