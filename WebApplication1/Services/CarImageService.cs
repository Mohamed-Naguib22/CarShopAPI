﻿using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Models;
namespace CarShopAPI.Services
{
    public class CarImageService : IImageService<Car>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CarImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetImageUrl(Car car)
        {
            if (car.ImgFile == null)
            {
                return "\\images\\No_Image.png";
            }
            else
            {
                string imgExtension = Path.GetExtension(car.ImgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                
                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    car.ImgFile.CopyTo(imgStream);
                }
                return car.Img_url = imgUrl;
            }
        }
        public void SetImage(Car car)
        {
            if (car.ImgFile == null)
            {
                car.Img_url = "\\images\\No_Image.png";
            }
            else
            {
                string imgExtension = Path.GetExtension(car.ImgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                car.Img_url = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    car.ImgFile.CopyTo(imgStream);
                }
            }
        }
        public void DeleteImage(Car car)
        {
            if (car.Img_url != @"\images\No_Image.png")
            {
                var imgOldPath = _webHostEnvironment.WebRootPath + car.Img_url;
                if (File.Exists(imgOldPath))
                {
                    File.Delete(imgOldPath);
                }
            }
        }
    }
}
