namespace DataAccess.Models
{
    public class MovementRequestHeader
    {
        public required Guid iMovementRequestHeader { get; set; }

        #region RequestedBy
        public required Guid iRequestedBy { get; set; }
        public int? CreatorNumber { get; set; }
        public string? CreatorFullName { get; set; }
        public string? CreatorEmail { get; set; }
        #endregion

        #region AuthorizedBy
        public Guid? iAuthorizedBy { get; set; }
        public int? AuthorizerNumber { get; set; }
        public string? AuthorizerFullName { get; set; }
        public string? AuthorizerEmail { get; set; }
        #endregion

        public int? MovementRequestHeaderNumber { get; set; }
        public string? MovementRequestHeaderDescription { get; set; }
        public required int MovementRequestHeaderStatus { get; set; }
        public required DateTime MovementRequestHeaderCreatedAt { get; set; }
        public DateTime? MovementRequestHeaderAuthorizedAt { get; set; }
    }
}
