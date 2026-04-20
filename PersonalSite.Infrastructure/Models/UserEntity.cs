namespace PersonalSite.Infrastructure.Models;

public class UserEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; } = true;
    public string Role { get; set; } = "Admin"; // Admin, SuperAdmin, etc.
    public DateTime? LastLoginOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}