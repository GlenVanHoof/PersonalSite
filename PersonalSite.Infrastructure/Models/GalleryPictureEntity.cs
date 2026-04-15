namespace PersonalSite.Infrastructure.Models;

public class GalleryPictureEntity
{
    public int Id { get; set; }
    public int Position { get; set; }
    public int PictureId { get; set; }

    // Navigation properties
    public PictureEntity? Picture { get; set; }
}