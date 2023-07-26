using Microsoft.Build.Framework;

namespace CarShopAPI.Models
{
    public class TokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
