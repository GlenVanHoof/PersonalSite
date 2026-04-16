using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Web.Models;

namespace PersonalSite.Web.Controllers;

public class PortfolioController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ICurrentLanguageService _languageService;

    public PortfolioController(
        IProjectService projectService,
        ICurrentLanguageService languageService)
    {
        _projectService = projectService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetProjectsOrderedAsync();

        var viewModel = new PortfolioViewModel
        {
            CurrentLanguage = _languageService.GetCurrentLanguageCode(),
            Projects = projects.Select(p => new PortfolioProjectViewModel
            {
                Id = p.Id,
                Slug = p.Slug,
                GitUrl = p.GithubUrl,
                ImagePath = p.ImagePath,
                OrderIndex = p.OrderIndex,
                Title = _languageService.GetTranslation(p.Title),
                ShortDescription = _languageService.GetTranslation(p.ShortDescription),
                LongDescription = _languageService.GetTranslation(p.Description),
                Technologies = null // TODO: Add technologies field if needed
            }).ToList()
        };

        return View(viewModel);
    }
}
