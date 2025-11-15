namespace DataAccess.Models
{
    public class Product
    {
        public required Guid iProduct { get; set; }
        public required string ProductCode { get; set; }
        public required string ProductDescription { get; set; }
        public required decimal ProductPrice { get; set; }
        public required Guid iManufacturer { get; set; }
        public string? ManufacturerCode { get; set; }
        public string? ManufacturerName { get; set; }
    }
}
