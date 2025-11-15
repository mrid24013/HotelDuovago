namespace DataAccess.Models
{
    public class Inventory
    {
        public required Guid iInventory { get; set; }

        #region Location
        public required Guid iLocation { get; set; }
        public int? LocationNumber { get; set; }
        public string? LocationName { get; set; }
        public string? LocationDescription { get; set; }
        public bool? LocationEnabled { get; set; }
        #endregion

        #region Product
        public required Guid iProduct { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public Guid? iManufacturer { get; set; }
        #endregion

        #region Created By
        public required Guid iCreatedBy { get; set; }
        public int? CreatorNumber { get; set; }
        public string? CreatorFullName { get; set; }
        public string? CreatorEmail { get; set; }
        #endregion

        public required int InventoryStock { get; set; }
        public required int InventoryMinStock { get; set; }
        public required int InventoryMaxStock { get; set; }
    }
}
