using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Models;

namespace PersoonlijkeSite.Controllers
{
    public class AboutController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly IEducationService _educationService;
        private readonly IExperienceService _experienceService;
        private readonly ICertificateService _certificateService;

        public AboutController(
            ISkillService skillService,
            IEducationService educationService,
            IExperienceService experienceService,
            ICertificateService certificateService)
        {
            _skillService = skillService;
            _educationService = educationService;
            _experienceService = experienceService;
            _certificateService = certificateService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AboutViewModel
            {
                PictureURL = "images/about/profile-picture.png",
                Skills = (await _skillService.GetAllSkillsAsync()).ToList(),
                Educations = (await _educationService.GetAllEducationsAsync()).ToList(),
                Experiences = (await _experienceService.GetAllExperiencesAsync()).ToList(),
                Certificates = (await _certificateService.GetAllCertificatesAsync()).ToList(),
                Description =
                "Ik ben Glen, een full‑stack .NET‑developer met een passie voor helder " +
                "gestructureerde code en praktische oplossingen. Dankzij mijn wetenschappelijke " +
                "opleiding en jarenlange ervaring in de logistiek kijk ik met een scherp analytisch" + 
                " oog naar processen en vertaal ik complexe problemen naar efficiënte software. " +
                "Ik bouw graag moderne, onderhoudbare applicaties in .NET en voel me thuis in teams" +
                " waar kwaliteit, samenwerking en continu leren centraal staan."
            };

            return View(viewModel);
        }
    }
}
