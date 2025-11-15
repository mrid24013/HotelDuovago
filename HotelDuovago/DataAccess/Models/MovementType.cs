namespace DataAccess.Models
{
    public class MovementType
    {
        public required Guid iMovementType { get; set; }
        public required int MovementTypeNumber { get; set; }
        public required string MovementTypeName { get; set; }
        public required string MovementTypeDescription { get; set; }
    }
}
