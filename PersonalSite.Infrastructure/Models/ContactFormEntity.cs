namespace PersonalSite.Infrastructure.Models;

public class ContactFormEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Message { get; set; }
    public DateTime CreatedOn { get; set; }
}