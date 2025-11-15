namespace DataAccess.Models
{
    public class InspectionDetail
    {
        public required Guid iInspectionDetail { get; set; }
        public required Guid iInspectionHeader { get; set; }

        #region Inventory
        public required Guid iInventory { get; set; }

        #region Location
        public Guid? iLocation { get; set; }
        public int? LocationNumber { get; set; }
        public string? LocationName { get; set; }
        #endregion

        #region Product
        public Guid? iProduct { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductDescription { get; set; }
        #endregion
        #endregion

        public required int InspectionDetailExpectedQuantity { get; set; }
        public required int? InspectionDetailRealQuantity { get; set; }
    }
}
