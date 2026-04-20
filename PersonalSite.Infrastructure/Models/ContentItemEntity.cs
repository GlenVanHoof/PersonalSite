namespace PersonalSite.Infrastructure.Models;

public class ContentItemEntity
{
    public int Id { get; set; }
    public required string ContentType { get; set; }
    public int ReferenceId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public ICollection<ContentFieldEntity> ContentFields { get; set; } = new List<ContentFieldEntity>();
}