namespace DataAccess.Models
{
    public class User
    {
        public required Guid iUser { get; set; }
        public int? UserNumber { get; set; }
        public required string UserFullName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public required Guid iUserRole { get; set; }
        public string? UserRoleName { get; set; }
        public string? UserRoleDescription { get; set; }
    }
}
