using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ILanguageService _languageService;

    public ProjectsController(IProjectService projectService, ILanguageService languageService)
    {
        _projectService = projectService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetProjectsOrderedAsync();
        return View(projects);
    }

    public async Task<IActionResult> Details(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new ProjectEditViewModel
        {
            Languages = languages.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var project = new Core.Models.Project
        {
            Slug = model.Slug,
            ImagePath = model.ImagePath,
            GithubUrl = model.GithubUrl,
            OrderIndex = model.OrderIndex,
            Title = model.Titles.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text),
            ShortDescription = model.ShortDescriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _projectService.CreateProjectAsync(project);
        TempData["SuccessMessage"] = "Project successfully created!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new ProjectEditViewModel
        {
            Id = project.Id,
            Slug = project.Slug,
            ImagePath = project.ImagePath,
            GithubUrl = project.GithubUrl,
            OrderIndex = project.OrderIndex,
            Languages = languages.ToList(),
            Titles = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = project.Title.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = project.Description.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            ShortDescriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = project.ShortDescription.GetValueOrDefault(l.Code, string.Empty)
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProjectEditViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var project = new Core.Models.Project
        {
            Id = model.Id,
            Slug = model.Slug,
            ImagePath = model.ImagePath,
            GithubUrl = model.GithubUrl,
            OrderIndex = model.OrderIndex,
            Title = model.Titles.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text),
            ShortDescription = model.ShortDescriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _projectService.UpdateProjectAsync(project);
        TempData["SuccessMessage"] = "Project successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        TempData["SuccessMessage"] = "Project successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}