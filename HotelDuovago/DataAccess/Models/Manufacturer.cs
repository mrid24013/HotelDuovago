namespace DataAccess.Models
{
    public class Manufacturer
    {
        public required Guid iManufacturer { get; set; }
        public required string ManufacturerCode { get; set; }
        public required string ManufacturerName { get; set; }
    }
}
