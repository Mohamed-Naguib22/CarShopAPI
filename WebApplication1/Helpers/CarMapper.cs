using CarShopAPI.Models;

namespace CarShopAPI.Helpers
{
    public static class CarMapper
    {
        public static CarDto MapCarToDto(Car car)
        {
            return new CarDto
            {
                CarId = car.CarId,
                Model = car.Model,
                Description = car.Description,
                Warranty = car.Warranty,
                Year = car.Year,
                Price = car.Price,
                IsNew = car.IsNew,
                Img_url = car.Img_url,
                BodyType = car.BodyType.Name,
                State = car.State.Name,
                Manufacturer = car.Manufacturer.Name,
                Username = car.ApplicationUser.FirstName
            };
        }
    }
}
