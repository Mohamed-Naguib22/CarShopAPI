using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class BodyTypeService : IEntityService<BodyType>
    {
        private readonly ApplicationDbContext _dbContext;
        public BodyTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetIdByNameAsync(string bodyTypeName)
        {
            var name = bodyTypeName.Trim().ToLower();

            var bodyType = await _dbContext.BodyTypes
                .SingleOrDefaultAsync(x => x.Name.ToLower() == name);

			return bodyType?.BodyTypeId ?? -1;
		}
		public async Task<List<string>> GetAllAsync()
        {
            var bodyTypes = await _dbContext.BodyTypes.OrderBy(x => x.Name)
                .Select(x => x.Name).ToListAsync();

            return bodyTypes;
        }
    }
}
