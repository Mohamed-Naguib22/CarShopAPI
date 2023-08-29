using CarShopAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CarShopAPI.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int carId);
        Task<Car> AddCarAsync(Car car);
        Task<Car> UpdateCarAsync(int carId, JsonPatchDocument<Car> patchDocument);
        Task<bool> RemoveCarAsync(int carId);
        Task<Car> SetImageAsync(IFormFile? imgFile, int carId);
    }
}