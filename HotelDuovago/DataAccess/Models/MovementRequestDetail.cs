namespace DataAccess.Models
{
    public class MovementRequestDetail
    {
        public required Guid iMovementRequestDetail { get; set; }

        #region Header
        public required Guid iMovementRequestHeader { get; set; }
        public int? MovementRequestHeaderNumber { get; set; }
        public string? MovementRequestHeaderDescription { get; set; }
        public int? MovementRequestHeaderStatus { get; set; }
        public DateTime? MovementRequestHeaderCreatedAt { get; set; }
        public DateTime? MovementRequestHeaderAuthorizedAt { get; set; }
        #endregion

        #region MovementType
        public required Guid iMovementType { get; set; }
        public int? MovementTypeNumber { get; set; }
        public string? MovementTypeName { get; set; }
        #endregion

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

        public string? MovementRequestDetailDescription { get; set; }
        public required int MovementRequestDetailQuantity { get; set; }
        public required int MovementRequestDetailStatus { get; set; }
    }
}
