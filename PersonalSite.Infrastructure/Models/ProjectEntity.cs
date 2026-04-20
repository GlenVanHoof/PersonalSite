namespace PersonalSite.Infrastructure.Models;

public class ProjectEntity
{
    public int Id { get; set; }
    public required string Slug { get; set; }
    public string? GithubUrl { get; set; }
    public string? ImagePath { get; set; }
    public int OrderIndex { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public ICollection<PictureEntity> Pictures { get; set; } = new List<PictureEntity>();
    public ICollection<ContentItemEntity> ContentItems { get; set; } = new List<ContentItemEntity>();
}
