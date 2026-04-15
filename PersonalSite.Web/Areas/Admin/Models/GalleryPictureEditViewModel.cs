using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class GalleryPictureEditViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Picture")]
    public int PictureId { get; set; }

    [Required]
    [Display(Name = "Position")]
    public int Position { get; set; }

    // Helper for view
    public List<Picture> AvailablePictures { get; set; } = new();
}