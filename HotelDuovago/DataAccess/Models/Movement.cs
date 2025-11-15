namespace DataAccess.Models
{
    public class Movement
    {
        public required Guid iMovement { get; set; }
        public required int MovementQuantity { get; set; }
        public required DateTime MovementCreatedAt { get; set; }

        #region Product
        public Guid? iProduct { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public Guid? iManufacturer { get; set; }
        #endregion

        #region FromInventory
        public Guid? iFromInventory { get; set; }

        #region Location
        public Guid? iFromInventoryLocation { get; set; }
        public int? FromInventoryLocationNumber { get; set; }
        public string? FromInventoryLocationName { get; set; }
        #endregion
        #endregion

        #region ToInventory
        public Guid? iToInventory { get; set; }

        #region Location
        public Guid? iToInventoryLocation { get; set; }
        public int? ToInventoryLocationNumber { get; set; }
        public string? ToInventoryLocationName { get; set; }
        #endregion
        #endregion

        #region MovementType
        public Guid? iMovementType { get; set; }
        public int? MovementTypeNumber { get; set; }
        public string? MovementTypeName { get; set; }
        #endregion

        #region MovementRequestDetail
        public Guid? iMovementRequestDetail { get; set; }
        public Guid? iMovementRequestHeader { get; set; }
        public int? MovementRequestHeaderNumber { get; set; }
        #endregion
    }
}
