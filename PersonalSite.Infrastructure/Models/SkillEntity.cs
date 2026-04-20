namespace PersonalSite.Infrastructure.Models;

public class SkillEntity
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public int ScoreOutOf100 { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public ICollection<ContentItemEntity> ContentItems { get; set; } = new List<ContentItemEntity>();
}
