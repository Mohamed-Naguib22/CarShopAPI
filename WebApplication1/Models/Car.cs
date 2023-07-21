using CarShopAPI.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarShopAPI.Models
{
    [ValidateNever]

    public class Car
    {
        public int CarId { get; set; }
        [Required, Range(1000, 9999, ErrorMessage = "Invalid year format")]
        [CurrentYearValidation(ErrorMessage = "Invalid year format")]
        public int Year { get; set; }
        [Required, Column(TypeName = "decimal(10,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price can not be negative!")]
        public decimal Price { get; set; }
        [Required]
        public string Model { get; set; }
        [Required, MaxLength(300, ErrorMessage = "Description can not be more than 300 characters")]
        public string Description { get; set; }
        [Required]
        public bool IsNew { get; set; }
        public int BodyTypeId { get; set; }
        [Required]
        public BodyType BodyType { get; set; }
        public int ManufacturerId { get; set; }
        [Required]
        public Manufacturer Manufacturer { get; set; }
        public int StateId { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public string SellerId { get; set; }
        [ValidateNever, JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
        [ValidateNever]
        [DisplayName("Image")]
        [DataType(DataType.ImageUrl)]
        public string Img_url { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public IFormFile ImgFile { get; set; }
    }
}
