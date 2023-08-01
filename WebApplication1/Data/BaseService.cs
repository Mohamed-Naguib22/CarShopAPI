using CarShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Data
{
    public class BaseService
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected IQueryable<CarDto> MapCarToDto()
        {
            return _dbContext.Cars
                .Include(c => c.BodyType)
                .Include(c => c.Manufacturer)
                .Include(c => c.State)
                .Include(c => c.ApplicationUser)
                .Select(car => new CarDto
                {
                    CarId = car.CarId,
                    Model = car.Model,
                    Description = car.Description,
                    Warranty = car.Warranty,
                    Year = car.Year,
                    Price = car.Price,
                    IsNew = car.IsNew,
                    Img_url = car.Img_url,
                    BodyType = car.BodyType.Name,
                    State = car.State.Name,
                    Manufacturer = car.Manufacturer.Name,
                    Username = car.ApplicationUser.FirstName
                });
        }
    }
}
