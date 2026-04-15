namespace PersonalSite.Infrastructure.Models;

public class PictureEntity
{
    public int Id { get; set; }
    public required string Source { get; set; }
    public int? ProjectId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    // Navigation properties
    public ProjectEntity? Project { get; set; }
    public ICollection<GalleryPictureEntity> GalleryPictures { get; set; } = new List<GalleryPictureEntity>();
}