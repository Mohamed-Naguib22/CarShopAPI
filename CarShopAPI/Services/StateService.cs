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
        public async Task<int> GetIdByNameAsync(string stateName)
        {
            var name = stateName.Trim().ToLower();

            var state = await _dbContext.States
                .SingleOrDefaultAsync(x => x.Name.ToLower() == name);

			return state?.StateId ?? -1;
		}
		public async Task<List<string>> GetAllAsync()
        {
            var states = await _dbContext.States.OrderBy(x => x.Name)
                .Select(x => x.Name).ToListAsync();

            return states;
        }
    }
}
