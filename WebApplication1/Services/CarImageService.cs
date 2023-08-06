using CarShopAPI.Implementation.Interfaces;
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
        public void SetImage(Car car, IFormFile? imgFile)
        {
            if (imgFile == null)
            {
                car.Img_url = "\\images\\No_Image.png";
            }
            else
            {
                DeleteImage(car);
                string imgExtension = Path.GetExtension(imgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                car.Img_url = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    imgFile.CopyTo(imgStream);
                }
            }
        }
        public void DeleteImage(Car car)
        {
            if (!string.IsNullOrEmpty(car.Img_url))
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
