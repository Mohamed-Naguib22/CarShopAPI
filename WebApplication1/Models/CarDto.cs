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
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return false;

            var other = (CarDto)obj;
            return CarId == other.CarId;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + CarId.GetHashCode();
            return hash;
        }
    }
}
