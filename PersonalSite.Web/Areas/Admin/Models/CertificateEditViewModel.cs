using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class CertificateEditViewModel
{
    public int Id { get; set; }

    [StringLength(200)]
    public string? Organisation { get; set; }

    [Display(Name = "Acquired On")]
    public DateTime? AcquiredOn { get; set; }

    // Translations
    public List<TranslationInputViewModel> Names { get; set; } = new();
    public List<TranslationInputViewModel> Descriptions { get; set; } = new();

    // Helper for view
    public List<Language> Languages { get; set; } = new();
}