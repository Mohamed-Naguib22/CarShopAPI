using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class StateService : IEntityService<State>
    {
        private readonly ApplicationDbContext _dbContext;
        public StateService(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public void GetId(Car car)
        {
            var name = car.State.Name.ToString().Trim();

            var state = _dbContext.States
                .FirstOrDefault(x => EF.Functions.Like(x.Name, $"{name}%"));

            if (state is null)
            {
                car.StateId = -1;
            }
            else
            {
                car.StateId = state.StateId;
            }
        }
        public List<string> GetAll()
        {
            var states = _dbContext.States.OrderBy(x => x.Name)
                .Select(x => x.Name).ToList();
            return states;
        }
    }
}
