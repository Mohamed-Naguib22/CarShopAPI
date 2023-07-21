using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Interfaces
{
    public interface ICarService
    {
        Task<object> GetAllCarsAsync(int pageNumber);
        Task<object> GetCarByIdAsync(int carId);
        Task<bool> AddCarAsync(Car car);
        Task<bool> UpdateCarAsync(Car newCar, int carId);
        Task<bool> RemoveCarAsync(int carId);
    }
}