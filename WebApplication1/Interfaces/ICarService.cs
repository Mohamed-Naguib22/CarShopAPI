using CarShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int carId);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(int carId, JsonPatchDocument<Car> patchDocument);
        Task<bool> RemoveCarAsync(int carId);
        IQueryable<Car> GetCarsWithRelatedEntities();
        Task<IEnumerable<CarDto>> GetRelatedCarsAsync(CarDto car);
    }
}