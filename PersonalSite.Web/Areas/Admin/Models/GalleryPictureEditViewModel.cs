using Microsoft.AspNetCore.Http;
using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class GalleryPictureEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please select an existing picture.")]
    public int? PictureId { get; set; }

    public List<Picture> AvailablePictures { get; set; } = new();
}