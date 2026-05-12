using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Web.Models;

namespace PersonalSite.Web.Controllers;

public class AboutController : Controller
{
    private readonly ISkillService _skillService;
    private readonly IEducationService _educationService;
    private readonly IExperienceService _experienceService;
    private readonly ICertificateService _certificateService;
    private readonly ICurrentLanguageService _languageService;

    public AboutController(
        ISkillService skillService,
        IEducationService educationService,
        IExperienceService experienceService,
        ICertificateService certificateService,
        ICurrentLanguageService languageService)
    {
        _skillService = skillService;
        _educationService = educationService;
        _experienceService = experienceService;
        _certificateService = certificateService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var skills = await _skillService.GetSkillsOrderedByTypeAsync();
        var educations = await _educationService.GetEducationsOrderedByDateAsync();
        var experiences = await _experienceService.GetExperiencesOrderedByDateAsync();
        var certificates = await _certificateService.GetCertificatesOrderedByDateAsync();
        Dictionary<string, List<SkillDisplayViewModel>> skillsByType = new();
        foreach (var skillGroup in skills)
        {
            skillsByType.Add(skillGroup.Key.ToString(), skillGroup.Value.Select(s => new SkillDisplayViewModel
            {
                Id = s.Id,
                Name = _languageService.GetTranslation(s.Name),
                Description = _languageService.GetTranslation(s.Description),
                ScoreOutOf100 = s.ScoreOutOf100
            }).ToList());

        }

        var viewModel = new AboutViewModel
        {
            PictureURL = "images/about/profile-picture.png",
            Skills = skillsByType,
            Educations = educations.Select(e => new EducationDisplayViewModel
            {
                Id = e.Id,
                Institution = _languageService.GetTranslation(e.Institution),
                Degree = _languageService.GetTranslation(e.Degree),
                FieldOfStudy = _languageService.GetTranslation(e.FieldOfStudy),
                Description = _languageService.GetTranslation(e.Description),
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToList(),
            Experiences = experiences.Select(ex => new ExperienceDisplayViewModel
            {
                Id = ex.Id,
                Company = _languageService.GetTranslation(ex.Company),
                Position = _languageService.GetTranslation(ex.Position),
                Description = _languageService.GetTranslation(ex.Description),
                StartDate = ex.StartDate,
                EndDate = ex.EndDate
            }).ToList(),
            Certificates = certificates.Select(c => new CertificateDisplayViewModel
            {
                Id = c.Id,
                Name = _languageService.GetTranslation(c.Name),
                Description = _languageService.GetTranslation(c.Description),
                Organisation = c.Organisation,
                AcquiredOn = c.AcquiredOn
            }).ToList(),
            Description = GetAboutDescription()
        };

        return View(viewModel);
    }

    private string GetAboutDescription()
    {
        // TODO: Later this should also come from database with translations
        var lang = _languageService.GetCurrentLanguageCode();
        return lang switch
        {
            "nl" => "Ik ben Glen, een full‑stack .NET‑developer met een passie voor helder " +
                    "gestructureerde code en praktische oplossingen. Dankzij mijn wetenschappelijke " +
                    "opleiding en jarenlange ervaring in de logistiek kijk ik met een scherp analytisch " +
                    "oog naar processen en vertaal ik complexe problemen naar efficiënte software. " +
                    "Ik bouw graag moderne, onderhoudbare applicaties in .NET en voel me thuis in teams " +
                    "waar kwaliteit, samenwerking en continu leren centraal staan.",
            "fr" => "Je suis Glen, un développeur full-stack .NET passionné par un code clairement " +
                    "structuré et des solutions pratiques. Grâce à ma formation scientifique et mes années " +
                    "d'expérience en logistique, j'analyse les processus avec un œil critique et transforme " +
                    "les problèmes complexes en logiciels efficaces. J'aime créer des applications modernes " +
                    "et maintenables en .NET et je m'épanouis dans des équipes où la qualité, la collaboration " +
                    "et l'apprentissage continu sont au cœur des préoccupations.",
            "de" => "Ich bin Glen, ein Full-Stack .NET-Entwickler mit einer Leidenschaft für klar " +
                    "strukturierten Code und praktische Lösungen. Dank meiner wissenschaftlichen Ausbildung " +
                    "und jahrelanger Erfahrung in der Logistik betrachte ich Prozesse mit analytischem Blick " +
                    "und übersetze komplexe Probleme in effiziente Software. Ich baue gerne moderne, " +
                    "wartbare Anwendungen in .NET und fühle mich in Teams wohl, in denen Qualität, " +
                    "Zusammenarbeit und kontinuierliches Lernen im Mittelpunkt stehen.",
            _ => "I'm Glen, a full-stack .NET developer with a passion for clearly structured code " +
                 "and practical solutions. Thanks to my scientific background and years of experience " +
                 "in logistics, I approach processes with an analytical eye and translate complex problems " +
                 "into efficient software. I enjoy building modern, maintainable applications in .NET and " +
                 "thrive in teams where quality, collaboration, and continuous learning are central."
        };
    }
}
