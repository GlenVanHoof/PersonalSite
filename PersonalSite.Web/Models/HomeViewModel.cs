namespace PersonalSite.Web.Models;

public class HomeViewModel
{
    public List<ProjectDisplayViewModel> Projects { get; set; } = new();
    public List<GalleryPictureViewModel> GalleryPictures { get; set; } = new();
}