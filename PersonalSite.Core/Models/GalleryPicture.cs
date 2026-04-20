namespace PersonalSite.Core.Models;

public class GalleryPicture
{
    public int Id { get; set; }
    public int Position { get; set; }
    public int PictureId { get; set; }
    
    // Navigation
    public Picture? Picture { get; set; }
}