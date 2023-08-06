using CarShopAPI.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShopAPI.Models
{
    public class Car
    {
        public int CarId { get; set; }
        [YearValidation(ErrorMessage = "Invalid year format")]
        public int Year { get; set; }
        [Required, Column(TypeName = "decimal(10,2)"), Range(0, double.MaxValue, ErrorMessage = "Price can not be negative!")]
        public decimal Price { get; set; }
        [Required]
        public string Model { get; set; }
        [Required, MaxLength(300, ErrorMessage = "Description can not be more than 300 characters")]
        public string Description { get; set; }
        [Required]
        public string Warranty { get; set; }
        [Required]
        public bool IsNew { get; set; }
        public int BodyTypeId { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public BodyType BodyType { get; set; }
        public int ManufacturerId { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public Manufacturer Manufacturer { get; set; }
        public int StateId { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public State State { get; set; }
        [Required]
        public string SellerId { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
        [ValidateNever, DisplayName("Image"), DataType(DataType.ImageUrl)]
        public string Img_url { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public IFormFile ImgFile { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public string Message { get; set; }
    }
}