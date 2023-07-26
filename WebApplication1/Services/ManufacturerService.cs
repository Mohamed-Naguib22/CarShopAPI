using CarShopAPI.Data;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class ManufacturerService : IEntityService<Manufacturer>
    {
        private readonly ApplicationDbContext _dbContext;
        public ManufacturerService(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<int> GetIdByNameAsync(string manufacturerName)
        {
            var name = manufacturerName.Trim().ToLower();

            var manufacturer = await _dbContext.Manufacturers
                .SingleOrDefaultAsync(x => x.Name.ToLower() == name);

            if (manufacturer is null)
            {
                return -1;
            }
            else
            {
                return manufacturer.ManufacturerId;
            }
        }
        public async Task<List<string>> GetAllAsync()
        {
            var manufacturers = await _dbContext.Manufacturers.OrderBy(x => x.Name)
                .Select(x => x.Name).ToListAsync();
            return manufacturers;
        }
    }
}
