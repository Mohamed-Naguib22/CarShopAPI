using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Filters
{
    public class StateFilterStrategy : ICarFilterStrategy
    {
        private readonly string _state;
        public StateFilterStrategy(string state)
        {
            _state = state;
        }
        public IQueryable<CarDto> ApplyFilter(IQueryable<CarDto> query)
        {
            return query.Where(car => car.State.ToLower().Contains(_state.ToLower()));
        }
    }
}
