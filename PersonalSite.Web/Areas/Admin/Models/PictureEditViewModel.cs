using Microsoft.AspNetCore.Http;
using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class PictureEditViewModel
{
    public int Id { get; set; }

    [Display(Name = "Upload Image")]
    public IFormFile? UploadedFile { get; set; }

    [Display(Name = "Or Enter Image URL")]
    [StringLength(500)]
    public string? Source { get; set; }

    [Display(Name = "Project (Optional)")]
    public int? ProjectId { get; set; }

    // Helper for view
    public List<Project> AvailableProjects { get; set; } = new();
    
    // For edit mode - show existing image
    public string? ExistingSource { get; set; }
}