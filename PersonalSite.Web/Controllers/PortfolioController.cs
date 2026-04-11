using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Models;

namespace PersoonlijkeSite.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly IProjectService _projectService;
        private readonly IProjectTranslationService _projectTranslationService;

        public PortfolioController(ILogger<PortfolioController> logger,
            IProjectService projectService, IProjectTranslationService projectTranslationService)
        {
            _logger = logger;
            _projectService = projectService;
            _projectTranslationService = projectTranslationService;
        }

        public async Task<IActionResult> Index(string lang = "en")
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var viewModel = new PortfolioViewModel
            {
                CurrentLanguage = lang
            };

            foreach (var project in projects.OrderBy(p => p.OrderIndex))
            {
                var translation = await _projectTranslationService.GetTranslationByProjectIdAndLanguageAsync(project.Id, lang)
                    ?? await _projectTranslationService.GetTranslationByProjectIdAndLanguageAsync(project.Id, "en");

                viewModel.Projects.Add(new PortfolioProjectViewModel
                {
                    Id = project.Id,
                    Slug = project.Slug,
                    GitUrl = project.GitUrl,
                    ImagePath = project.ImagePath,
                    OrderIndex = project.OrderIndex,
                    Title = translation?.Title,
                    ShortDescription = translation?.ShortDescription,
                    LongDescription = translation?.LongDescription,
                    Technologies = translation?.Technologies
                });
            }

            return View(viewModel);
        }
    }
}
