using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShopAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public List<Car> Cars { get; set; }
        [ValidateNever]
        [DisplayName("Image")]
        [DataType(DataType.ImageUrl)]
        public string Img_url { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public IFormFile ImgFile { get; set; }
    }
}
