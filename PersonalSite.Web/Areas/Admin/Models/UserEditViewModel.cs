using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Areas.Admin.Models;

public class UserEditViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    [StringLength(100)]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Required]
    [StringLength(50)]
    public string Role { get; set; } = "Admin";

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    // Password only required for create
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }
}