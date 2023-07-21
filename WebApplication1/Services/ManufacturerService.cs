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
        public void GetId(Car car)
        {
            var name = car.Manufacturer.Name.ToString().Trim();

            var manufacturer = _dbContext.Manufacturers
                .FirstOrDefault(x => EF.Functions.Like(x.Name, $"{name}%"));

            if (manufacturer is null)
            {
                car.ManufacturerId = -1;
            }
            else
            {
                car.ManufacturerId = manufacturer.ManufacturerId;
            }
        }
        public List<string> GetAll()
        {
            var manufacturers =  _dbContext.Manufacturers.OrderBy(x => x.Name)
                .Select(x => x.Name).ToList();
            return manufacturers;
        }
    }
}
