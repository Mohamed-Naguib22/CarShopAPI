using CarShopAPI.Helpers;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CarShopAPI.Interfaces;

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

        [HttpGet("page/{pageNumber}")]
        public async Task<IActionResult> GetAllCarsAsync(int pageNumber)
        {
            var cars = await _carService.GetAllCarsAsync(pageNumber);
            return Ok(cars);
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarByIdAsync(int carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);
            if (car is null)
            {
                return NotFound(new { Message ="Car is Not Found"});
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarAsync([FromForm] Car car)
        {
            var validationErrors = ValidationHelper<Car>.Validate(car);
            if (validationErrors.Count != 0)
            {
                return BadRequest(new { validationErrors });
            }

            bool success = await _carService.AddCarAsync(car);
            if (!success)
            {
                return BadRequest(new { Message =  "Please Enter Valid Car Data" });
            }

            return Ok(new { Message = "Car added successfully" , Car = car });
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCarAsync([FromBody] Car newCar, int carId)
        {
            var errors = ValidationHelper<Car>.Validate(newCar);
            if (errors.Count != 0)
            {
                return BadRequest(errors);
            }

            bool success = await _carService.UpdateCarAsync(newCar, carId);
            if (!success)
            {
                return NotFound(new { Message =  "Car is Not Found" });
            }
            return Ok(new { Message ="Car Details Updated Successfully", Car = newCar });
        }

        [HttpDelete("{carId}")]
        public async Task<IActionResult> RemoveCarAsync(int carId)
        {
            bool success = await _carService.RemoveCarAsync(carId);
            if (!success)
            {
                return NotFound(new { Message = "Car is Not Found" });
            }
            return Ok(new { Message = "Car Removed Successfully" });
        }
    }
}
