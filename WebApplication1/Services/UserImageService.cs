using CarShopAPI.Implementation.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Services
{
    public class UserImageService: IImageService<ApplicationUser>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetImageUrl(ApplicationUser user)
        {
            if (user.ImgFile == null)
            {
                return "\\images\\No_Image.png";
            }
            else
            {
                string imgExtension = Path.GetExtension(user.ImgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    user.ImgFile.CopyTo(imgStream);
                }
                return user.Img_url = imgUrl;
            }
        }
        public void SetImage(ApplicationUser user)
        {
            if (user.ImgFile == null)
            {
                user.Img_url = "\\images\\No_Image.png";
            }

            else
            {
                DeleteImage(user);
                string imgExtension = Path.GetExtension(user.ImgFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                user.Img_url = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                using (var imgStream = new FileStream(imgPath, FileMode.Create))
                {
                    user.ImgFile.CopyTo(imgStream);
                }
            }
        }
        public void DeleteImage(ApplicationUser user)
        {
            var imgOldPath = _webHostEnvironment.WebRootPath + user.Img_url;
            if (File.Exists(imgOldPath))
            {
                File.Delete(imgOldPath);
            }
        }
    }
}
