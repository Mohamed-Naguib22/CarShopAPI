using CarShopAPI.Data;
using CarShopAPI.Helpers;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEntityService<BodyType> _bodyTypeService;
        private readonly IEntityService<Manufacturer> _manufacturerService;
        private readonly IEntityService<State> _stateService;
        private readonly IImageService<Car> _carImageService;
        public CarService(ApplicationDbContext dbContext, IEntityService<BodyType> bodyTypeService,
            IEntityService<Manufacturer> manufacturerService, IEntityService<State> stateService,
            IImageService<Car> carImageService)
        {
            _dbContext = dbContext;
            _bodyTypeService = bodyTypeService;
            _manufacturerService = manufacturerService;
            _stateService = stateService;
            _carImageService = carImageService;
            _carImageService = carImageService;
        }
        private async Task<IEnumerable<CarDto>> GetRelatedCarsAsync(CarDto car)
        {
            var allCars = await GetAllCarsAsync();

            var relatedCars = allCars.Where(x => x.BodyType.Equals(car.BodyType))
                .Take(8).Except(new List<CarDto> { car });

            return relatedCars;
        }
        public IQueryable<Car> GetCarsWithRelatedEntities()
        {
            return _dbContext.Cars
                .Include(c => c.BodyType)
                .Include(c => c.Manufacturer)
                .Include(c => c.State)
                .Include(c => c.ApplicationUser);
        }
        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var allCars = await GetCarsWithRelatedEntities()
                 .Select(car => CarMapper.MapCarToDto(car)).ToListAsync();

            return allCars;
        }
        public async Task<CarDto> GetCarByIdAsync(int carId)
        {
            var car = await GetCarsWithRelatedEntities()
                 .FirstOrDefaultAsync(car => car.CarId == carId);

            if (car is null)
            {
                return null;
            }
            var carDto = CarMapper.MapCarToDto(car);
            return carDto;
        }
        public async Task<bool> AddCarAsync(Car car)
        {
            int bodyTypeId = await _bodyTypeService.GetIdByNameAsync(car.BodyType.Name);
            int stateId = await _stateService.GetIdByNameAsync(car.State.Name);
            int manufacturerId = await _manufacturerService.GetIdByNameAsync(car.Manufacturer.Name);
            string imgUrl = _carImageService.GetImageUrl(car);

            if (bodyTypeId == -1 || stateId == -1 || manufacturerId == -1)
            {
                return false;
            }

            await _dbContext.AddAsync(new Car
            {
                Model = car.Model,
                Description = car.Description,
                Year = car.Year,
                Price = car.Price,
                IsNew = car.IsNew,
                Warranty = car.Warranty,
                SellerId = car.SellerId,
                BodyTypeId = bodyTypeId,
                StateId = stateId,
                ManufacturerId = manufacturerId,
                Img_url = imgUrl,
            });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCarAsync([FromBody] Car newCar, int carId)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

            if (car is null)
            {
                return false;
            }
            int bodyTypeId = await _bodyTypeService.GetIdByNameAsync(car.BodyType.Name);
            int stateId = await _stateService.GetIdByNameAsync(car.State.Name);
            int manufacturerId = await _manufacturerService.GetIdByNameAsync(car.Manufacturer.Name);

            if (bodyTypeId == -1 || stateId == -1 || manufacturerId == -1)
            {
                return false;
            }

            car.Year = newCar.Year;
            car.IsNew = newCar.IsNew;
            car.Model = newCar.Model;
            car.Description = newCar.Description;
            car.Price = newCar.Price;
            car.BodyTypeId = newCar.BodyTypeId;
            car.StateId = newCar.StateId;
            car.ManufacturerId = newCar.ManufacturerId;
            car.Img_url = newCar.Img_url;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveCarAsync(int carId)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

            if (car is null)
            {
                return false;
            }

            _carImageService.DeleteImage(car);
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}