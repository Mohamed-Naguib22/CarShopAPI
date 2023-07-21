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
        public void GetId(Car car)
        {
            var name = car.BodyType.Name.ToString().Trim();

            var bodyType = _dbContext.BodyTypes
                .FirstOrDefault(x => EF.Functions.Like(x.Name, $"{name}%"));

            if (bodyType is null)
            {
                car.BodyTypeId = -1;
            }
            else
            {
                car.BodyTypeId = bodyType.BodyTypeId;
            }
        }
        public List<string> GetAll()
        {
            var bodyTypes = _dbContext.BodyTypes.OrderBy(x => x.Name)
                .Select(x => x.Name).ToList();
            return bodyTypes;
        }
    }
}
