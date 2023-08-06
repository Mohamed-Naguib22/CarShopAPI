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
			var query = from car in _dbContext.Cars

			join bodyType in _dbContext.BodyTypes
			on car.BodyTypeId equals bodyType.BodyTypeId

			join state in _dbContext.States
			on car.StateId equals state.StateId

			join manufacturer in _dbContext.Manufacturers
			on car.ManufacturerId equals manufacturer.ManufacturerId

			join user in _dbContext.Users
			on car.SellerId equals user.Id

			select new CarDto
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
			};
			return query;
		}
    }
}
