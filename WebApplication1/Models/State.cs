using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace CarShopAPI.Models
{
    public class State
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        [ValidateNever, JsonIgnore]
        public List<Car> Cars { get; set; }
    }
}