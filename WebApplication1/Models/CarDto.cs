using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarShopAPI.Models
{
    public class CarDto
    {
        public int CarId { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Warranty { get; set; }
        public string Img_url { get; set; }
        public string BodyType { get; set; }
        public string State { get; set; }
        public string Manufacturer { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public IEnumerable<CarDto> RelatedCars { get; set; }
        [NotMapped, ValidateNever, JsonIgnore]
        public string Message { get; set; }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return false;

            if (obj is not CarDto other) return false;
            return CarId == other.CarId;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + CarId.GetHashCode();
            return hash;
        }
        public bool ShouldSerializeRelatedCars()
        {
            return RelatedCars is not null;
        }
        public bool ShouldSerializeMessage()
        {
            return Message is not null;
        }
    }
}
