using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Services
{
    public class UserImageService : IImageService<ApplicationUser>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void SetImage(ApplicationUser user, IFormFile? imgFile)
        {
            if (imgFile == null)
            {
                user.Img_url = "\\images\\No_Image.png";
            }
            else
            {
                DeleteImage(user);
                string imgExtension = Path.GetExtension(imgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                user.Img_url = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    imgFile.CopyTo(imgStream);
                }
            }
        }
        public void DeleteImage(ApplicationUser user)
        {
            if (!string.IsNullOrEmpty(user.Img_url))
            {
                var imgOldPath = _webHostEnvironment.WebRootPath + user.Img_url;
                if (File.Exists(imgOldPath))
                {
                    File.Delete(imgOldPath);
                }
            }
        }
    }
}
