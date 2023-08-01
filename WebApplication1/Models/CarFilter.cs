using CarShopAPI.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Models
{
    [ValidateNever]
    public class CarFilter
    {
        [Range(0, double.MaxValue, ErrorMessage = "Price can not be negative!")]
        public decimal? Price { get; set; }
        [YearValidation(ErrorMessage = "Invalid year format")]
        public int? Year { get; set; }
        public string? Model { get; set; }
        public string? State { get; set; }
        public string? Manufacturer { get; set; }
        public string? BodyType { get; set; }
        public bool? IsNew { get; set; }
    }
}
