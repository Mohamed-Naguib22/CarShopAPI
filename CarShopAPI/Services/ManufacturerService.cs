using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class ManufacturerService : BaseService, IEntityService<Manufacturer>
    {
        public ManufacturerService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<int> GetIdByNameAsync(string manufacturerName)
        {
            var name = manufacturerName.Trim().ToLower();

            var manufacturer = await _dbContext.Manufacturers
                .SingleOrDefaultAsync(x => x.Name.ToLower() == name);

			return manufacturer?.ManufacturerId ?? -1;

		}
		public async Task<List<string>> GetAllAsync()
        {
            var manufacturers = await _dbContext.Manufacturers.OrderBy(x => x.Name)
                .Select(x => x.Name).ToListAsync();

            return manufacturers;
        }
    }
}
