namespace DataAccess.Models
{
    public class UserRole
    {
        public required Guid iUserRole { get; set; }
        public required string UserRoleName { get; set; }
        public string? UserRoleDescription { get; set; }
    }
}
