using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace CarShopAPI.Models
{
    public class TokenModel
    {
        [Required]
        public string Token { get; set; }
        [JsonIgnore, ValidateNever]
        public string Message { get; set; }
    }
}
