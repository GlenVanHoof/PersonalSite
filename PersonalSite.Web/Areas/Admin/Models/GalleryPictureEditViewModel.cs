using Microsoft.AspNetCore.Http;
using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class GalleryPictureEditViewModel
{
    public int Id { get; set; }

    [Display(Name = "Upload Image")]
    public IFormFile? UploadedFile { get; set; }

    [Display(Name = "Or Enter Image URL")]
    [StringLength(500)]
    public string? Source { get; set; }

    [Required]
    [Display(Name = "Position")]
    [Range(1, 1000)]
    public int Position { get; set; }

    // For edit mode - existing picture info
    public int? PictureId { get; set; }
    public string? ExistingSource { get; set; }

    // For backward compatibility - list of existing pictures to choose from
    public List<Picture> AvailablePictures { get; set; } = new();
    public bool UseExistingPicture { get; set; } = false;
}