using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class PictureEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "Image Source (URL or Path)")]
    public string Source { get; set; } = string.Empty;

    [Display(Name = "Project (Optional)")]
    public int? ProjectId { get; set; }

    // Helper for view
    public List<Project> AvailableProjects { get; set; } = new();
}