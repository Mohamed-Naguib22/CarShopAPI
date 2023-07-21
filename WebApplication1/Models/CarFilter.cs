namespace CarShopAPI.Models
{
    public class CarFilter
    {
        public decimal? Price { get; set; }
        public int? Year { get; set; }
        public string? Model { get; set; }
        public string? State { get; set; }
        public string? Manufacturer { get; set; }
        public string? BodyType { get; set; }
        public bool? IsNew { get; set; }
    }
}
