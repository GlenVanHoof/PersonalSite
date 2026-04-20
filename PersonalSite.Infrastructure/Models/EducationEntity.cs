namespace PersonalSite.Infrastructure.Models;

public class EducationEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public ICollection<ContentItemEntity> ContentItems { get; set; } = new List<ContentItemEntity>();
}
