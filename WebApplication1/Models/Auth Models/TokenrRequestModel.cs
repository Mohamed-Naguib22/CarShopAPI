using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Models
{
    public class TokenrRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}