using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class ExperienceEditViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    // Translations
    public List<TranslationInputViewModel> Companies { get; set; } = new();
    public List<TranslationInputViewModel> Positions { get; set; } = new();
    public List<TranslationInputViewModel> Descriptions { get; set; } = new();

    // Helper for view
    public List<Language> Languages { get; set; } = new();
}