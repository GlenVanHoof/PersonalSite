using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        [StringLength(50, ErrorMessage = "Voornaam mag maximaal 50 karakters bevatten")]
        [Display(Name = "Voornaam")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        [StringLength(50, ErrorMessage = "Achternaam mag maximaal 50 karakters bevatten")]
        [Display(Name = "Achternaam")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "E-mailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Voer een geldig e-mailadres in")]
        [Display(Name = "E-mailadres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Bericht is verplicht")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Bericht moet tussen de 10 en 1000 karakters bevatten")]
        [Display(Name = "Bericht")]
        public string? Message { get; set; }
    }
}