using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Web.Models;

namespace PersonalSite.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IGalleryPictureService _galleryPictureService;
    private readonly ICurrentLanguageService _languageService;

    public HomeController(
        IProjectService projectService,
        IGalleryPictureService galleryPictureService,
        ICurrentLanguageService languageService)
    {
        _projectService = projectService;
        _galleryPictureService = galleryPictureService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetProjectsOrderedAsync();
        var galleryPictures = await _galleryPictureService.GetGalleryPicturesOrderedAsync();

        var viewModel = new HomeViewModel
        {
            Projects = projects.Select(p => new ProjectDisplayViewModel
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = _languageService.GetTranslation(p.Title),
                ShortDescription = _languageService.GetTranslation(p.ShortDescription),
                ImagePath = p.ImagePath,
                GithubUrl = p.GithubUrl,
                ProjectUrl = p.ProjectUrl,
                Skills = p.Skills?.Select(s => _languageService.GetTranslation(s.Name)).ToList()
            }).ToList(),
            GalleryPictures = galleryPictures.Select(gp => new GalleryPictureViewModel
            {
                Id = gp.Id,
                Position = gp.Position,
                ImageSource = gp.Picture?.Source ?? string.Empty
            }).ToList()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Project(string slug)
    {
        var project = await _projectService.GetProjectBySlugAsync(slug);

        if (project == null)
            return NotFound();

        var viewModel = new ProjectDetailViewModel
        {
            Id = project.Id,
            Slug = project.Slug,
            Title = _languageService.GetTranslation(project.Title),
            Description = _languageService.GetTranslation(project.Description),
            ShortDescription = _languageService.GetTranslation(project.ShortDescription),
            ImagePath = project.ImagePath,
            GithubUrl = project.GithubUrl,
            ProjectUrl = project.ProjectUrl,
            Pictures = project.Pictures.Select(p => p.Source).ToList(),
            Skills = project.Skills?.Select(s => _languageService.GetTranslation(s.Name)).ToList() ?? new List<string>()
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
