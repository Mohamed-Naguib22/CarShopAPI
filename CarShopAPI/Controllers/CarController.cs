﻿using CarShopAPI.Helpers;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using CarShopAPI.Interfaces;
using CarShopAPI.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using CarShopAPI.Services;

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
            var cars = (await _carService.GetAllCarsAsync())
                .Paginate(pageNumber, 10);

            return Ok(cars);
        }
        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarByIdAsync(int carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);

            if (!string.IsNullOrEmpty(car.Message))
                return NotFound(car.Message);

            return Ok(car);
        }
        [HttpPost]
        public async Task<IActionResult> AddCarAsync([FromForm] Car car)
        {
            var addedCar = await _carService.AddCarAsync(car);

            if (!string.IsNullOrEmpty(addedCar.Message))
                return BadRequest(addedCar.Message);

            return Ok(addedCar);
        }
        [HttpPatch("{carId}")]
        public async Task<IActionResult> UpdateCarAsync(int carId, [FromBody] JsonPatchDocument<Car> patchDocument)
        {
            var car = await _carService.UpdateCarAsync(carId, patchDocument);

            if (!string.IsNullOrEmpty(car.Message))
                return BadRequest(car.Message);

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
        [HttpPut("{carId}")]
        public async Task<IActionResult> SetImageAsync([FromForm] IFormFile? imgFile, int carId)
        {
            var user = await _carService.SetImageAsync(imgFile, carId);

            if (!string.IsNullOrEmpty(user.Message))
                return BadRequest(user.Message);

            return Ok(user);
        }
    }
}
