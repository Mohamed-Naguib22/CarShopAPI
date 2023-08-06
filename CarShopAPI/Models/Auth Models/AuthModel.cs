using Newtonsoft.Json;

namespace CarShopAPI.Models
{
    public class AuthModel
    {
        [JsonIgnore]
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
