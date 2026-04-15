using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class SkillEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [Range(0, 100)]
    [Display(Name = "Score (0-100)")]
    public int ScoreOutOf100 { get; set; }

    // Translations
    public List<TranslationInputViewModel> Names { get; set; } = new();
    public List<TranslationInputViewModel> Descriptions { get; set; } = new();

    // Helper for view
    public List<Language> Languages { get; set; } = new();
}