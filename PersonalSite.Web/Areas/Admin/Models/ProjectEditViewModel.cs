using PersonalSite.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class ProjectEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Slug { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "GitHub URL")]
    public string? GithubUrl { get; set; }

    [Display(Name = "Order Index")]
    public int OrderIndex { get; set; }

    // Image upload
    [Display(Name = "Upload Images")]
    public List<IFormFile>? UploadedImages { get; set; }

    // For managing existing images
    public List<Picture> ExistingPictures { get; set; } = new();

    [Display(Name = "Card Image")]
    public string? SelectedCardImagePath { get; set; }

    public List<int>? PicturesToDelete { get; set; }

    // Translations
    public List<TranslationInputViewModel> Titles { get; set; } = new();
    public List<TranslationInputViewModel> Descriptions { get; set; } = new();
    public List<TranslationInputViewModel> ShortDescriptions { get; set; } = new();

    // Helper for view
    public List<Language> Languages { get; set; } = new();
}