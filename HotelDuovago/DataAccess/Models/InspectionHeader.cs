namespace DataAccess.Models
{
    public class InspectionHeader
    {
        public required Guid iInspectionHeader { get; set; }

        #region Created By
        public required Guid iCreatedBy { get; set; }
        public int? CreatorNumber { get; set; }
        public string? CreatorFullName { get; set; }
        public string? CreatorEmail { get; set; }
        #endregion

        #region Location
        public required Guid iLocation { get; set; }
        public int? LocationNumber { get; set; }
        public string? LocationName { get; set; }
        public string? LocationDescription { get; set; }
        public bool? LocationEnabled { get; set; }
        #endregion

        public int? InspectionHeaderNumber { get; set; }
        public required string InspectionHeaderDescription { get; set; }
        public required DateTime InspectionHeaderCreatedAt { get; set; }
    }
}
