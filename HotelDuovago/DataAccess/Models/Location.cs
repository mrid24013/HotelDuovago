namespace DataAccess.Models
{
    public class Location
    {
        public required Guid iLocation { get; set; }
        public int? LocationNumber { get; set; }
        public required string LocationName { get; set; }
        public string? LocationDescription { get; set; }
        public bool? LocationEnabled { get; set; }
    }
}
