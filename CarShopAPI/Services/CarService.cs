using CarShopAPI.Data;
using CarShopAPI.Helpers;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Services
{
    public class CarService : BaseService, ICarService
    {
        private readonly IEntityService<BodyType> _bodyTypeService;
        private readonly IEntityService<Manufacturer> _manufacturerService;
        private readonly IEntityService<State> _stateService;
        private readonly IImageService<Car> _carImageService;
        public CarService(ApplicationDbContext dbContext, IEntityService<BodyType> bodyTypeService,
            IEntityService<Manufacturer> manufacturerService, IEntityService<State> stateService,
            IImageService<Car> carImageService) : base (dbContext)
        {
            _bodyTypeService = bodyTypeService;
            _manufacturerService = manufacturerService;
            _stateService = stateService;
            _carImageService = carImageService;
        }
        private async Task<IEnumerable<CarDto>> GetRelatedCarsAsync(CarDto car)
        {
            var allCars = await GetAllCarsAsync();

            var relatedCars = allCars
                .Where(c => c.BodyType.Equals(car.BodyType))
                .Take(8).Except(new List<CarDto> { car });

            return relatedCars;
        }
        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var allCars = await MapCarToDto().ToListAsync();

            return allCars;
        }
        public async Task<CarDto> GetCarByIdAsync(int carId)
        {
            var car = await MapCarToDto().FirstOrDefaultAsync(car => car.CarId == carId);

            if (car is null)
                return new CarDto { Message = "Car is not found" };

            car.RelatedCars = await GetRelatedCarsAsync(car);
            return car;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
			var validationErrors = ValidationHelper<Car>.Validate(car);
			if (!string.IsNullOrEmpty(validationErrors))
				return new Car { Message = validationErrors };

			_carImageService.SetImage(car, car.ImgFile);
            car.ManufacturerId = await _manufacturerService.GetIdByNameAsync(car.Manufacturer.Name);
			car.StateId = await _stateService.GetIdByNameAsync(car.State.Name);
			car.BodyTypeId = await _bodyTypeService.GetIdByNameAsync(car.BodyType.Name);

			await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();
            return car;
        }
        public async Task<Car> UpdateCarAsync(int carId, JsonPatchDocument<Car> patchDocument)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

            if (car is null)
                return new Car { Message = "Car is not found"};

            patchDocument.ApplyTo(car);

            var validationErrors = ValidationHelper<Car>.Validate(car);
            if (!string.IsNullOrEmpty(validationErrors))
                return new Car { Message = validationErrors };

            await _dbContext.SaveChangesAsync();
            return car;
        }
        public async Task<bool> RemoveCarAsync(int carId)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

            if (car is null)
                return false;

            _carImageService.DeleteImage(car);
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Car> SetImageAsync(IFormFile? imgFile, int carId)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.CarId == carId);

            if (car is null)
                return new Car { Message = "Car is not found" };

            _carImageService.SetImage(car, imgFile);

            await _dbContext.SaveChangesAsync();
            return car;
        }
    }
}
