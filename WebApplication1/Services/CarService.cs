using CarShopAPI.Data;
using CarShopAPI.Helpers;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.XPath;

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
        public async Task<IEnumerable<CarDto>> GetRelatedCarsAsync(CarDto car)
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

            var carDto = CarMapper.MapCarToDto(car);
            return carDto;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            int bodyTypeId = await _bodyTypeService.GetIdByNameAsync(car.BodyType.Name);
            int stateId = await _stateService.GetIdByNameAsync(car.State.Name);
            int manufacturerId = await _manufacturerService.GetIdByNameAsync(car.Manufacturer.Name);
            string imgUrl = _carImageService.GetImageUrl(car);

            var carDto = new Car
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
            };
            await _dbContext.AddAsync(carDto);
            await _dbContext.SaveChangesAsync();
            return carDto;
        }
        public async Task<Car> UpdateCarAsync(int carId, JsonPatchDocument<Car> patchDocument)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

            if (car is null)
                return new Car { Message = "Car is not found"};

            patchDocument.ApplyTo(car);
            await _dbContext.SaveChangesAsync();
            return car;
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