using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class BodyTypeService : BaseService, IEntityService<BodyType>
    {
        public BodyTypeService(ApplicationDbContext dbContext) : base(dbContext)
        {
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
