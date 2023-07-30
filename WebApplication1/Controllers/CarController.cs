using CarShopAPI.Helpers;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CarShopAPI.Interfaces;
using CarShopAPI.Extensions;
using Microsoft.AspNetCore.JsonPatch;

namespace CarShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarByIdAsync(int carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);

            if (car is null)
                return NotFound("Car is not found");
            
            var cars = await _carService.GetRelatedCarsAsync(car);
            return Ok(new { Car = car , RelatedCars = cars});
        }

        [HttpPost]
        public async Task<IActionResult> AddCarAsync([FromForm] Car car)
        {
            var validationErrors = ValidationHelper<Car>.Validate(car);
            if (!string.IsNullOrEmpty(validationErrors))
                return BadRequest(validationErrors);

            var carDto = await _carService.AddCarAsync(car);
            return Ok(carDto);
        }

        [HttpPatch("{carId}")]
        public async Task<IActionResult> UpdateCarAsync(int carId, [FromBody] JsonPatchDocument<Car> patchDocument)
        {
            var car = await _carService.UpdateCarAsync(carId, patchDocument);

            if (!string.IsNullOrEmpty(car.Message))
                return NotFound(car.Message);

            var validationErrors = ValidationHelper<Car>.Validate(car);
            if (!string.IsNullOrEmpty(validationErrors))
                return BadRequest(validationErrors);

            return Ok(car);
        }

        [HttpDelete("{carId}")]
        public async Task<IActionResult> RemoveCarAsync(int carId)
        {
            bool success = await _carService.RemoveCarAsync(carId);
            if (!success)
                return NotFound("Car is Not Found");

            return Ok("Car removed successfully");
        }
    }
}
