using CarShopAPI.Data;
using CarShopAPI.Extensions;
using CarShopAPI.Helpers;
using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        public async Task<object> GetAllCarsAsync(int pageNumber)
        {
            var allCars = await _dbContext.Cars
                .Include(c => c.BodyType)
                .Include(c => c.Manufacturer)
                .Include(c => c.State)
                 .Select(car => new
                 {
                     car.CarId,
                     car.Model,
                     car.Description,
                     car.Year,
                     car.Price,
                     car.IsNew,
                     car.SellerId,
                     car.Img_url,
                     BodyType = car.BodyType.Name,
                     State = car.State.Name,
                     Manufacturer = car.Manufacturer.Name
                 })
                 .ToListAsync();

            return allCars.Pagenate(pageNumber, 30);
        }
        public async Task<object> GetCarByIdAsync(int carId)
        {
            var car = await _dbContext.Cars
                .Where(x => x.CarId == carId)
                .Include(c => c.BodyType)
                .Include(c => c.Manufacturer)
                .Include(c => c.State)
                 .Select(car => new
                 {
                     car.CarId,
                     car.Model,
                     car.Description,
                     car.Year,
                     car.Price,
                     car.IsNew,
                     car.SellerId,
                     BodyType = car.BodyType.Name,
                     State = car.State.Name,
                     Manufacturer = car.Manufacturer.Name
                 })
                .FirstOrDefaultAsync();

            return car;
        }
        public async Task<bool> AddCarAsync(Car car)
        {
            _bodyTypeService.GetId(car);
            _stateService.GetId(car);
            _manufacturerService.GetId(car);

            if(car.BodyTypeId == -1|| car.StateId == -1 || car.ManufacturerId == -1)
            {
                return false;
            }
            
            _carImageService.SetImage(car);

            await _dbContext.AddAsync(car);
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
            _bodyTypeService.GetId(newCar);
            _stateService.GetId(newCar);
            _manufacturerService.GetId(newCar);

            if (car.BodyTypeId == -1 || car.StateId == -1 || car.ManufacturerId == -1)
            {
                return false;
            }

            _carImageService.UpdateImage(car);

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