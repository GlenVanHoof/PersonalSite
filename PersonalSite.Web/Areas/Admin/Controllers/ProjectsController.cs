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
    private readonly IPictureService _pictureService;
    private readonly ILanguageService _languageService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProjectsController(
        IProjectService projectService,
        IPictureService pictureService,
        ILanguageService languageService,
        IWebHostEnvironment webHostEnvironment)
    {
        _projectService = projectService;
        _pictureService = pictureService;
        _languageService = languageService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return View(projects);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var languages = await _languageService.GetAllLanguagesAsync();
        var model = new ProjectEditViewModel
        {
            Languages = languages.ToList(),
            Titles = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name
            }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name
            }).ToList(),
            ShortDescriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name
            }).ToList()
        };
        return View(model);
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
            GithubUrl = model.GithubUrl,
            OrderIndex = model.OrderIndex,
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
            Title = model.Titles.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text),
            ShortDescription = model.ShortDescriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        var createdProject = await _projectService.CreateProjectAsync(project);

        // Handle image uploads
        if (model.UploadedImages?.Any() == true)
        {
            string? firstImagePath = null;

            foreach (var file in model.UploadedImages)
            {
                var imagePath = await SaveUploadedImageAsync(file);
                if (imagePath == null) continue;

                var picture = new Core.Models.Picture
                {
                    Source = imagePath,
                    ProjectId = createdProject.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };

                await _pictureService.CreatePictureAsync(picture);

                // Track first image for card
                firstImagePath ??= imagePath;
            }

            // Set first uploaded image as card image if none selected
            if (!string.IsNullOrEmpty(firstImagePath))
            {
                createdProject.ImagePath = model.SelectedCardImagePath ?? firstImagePath;
                await _projectService.UpdateProjectAsync(createdProject);
            }
        }

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
        var model = new ProjectEditViewModel
        {
            Id = project.Id,
            Slug = project.Slug,
            GithubUrl = project.GithubUrl,
            OrderIndex = project.OrderIndex,
            SelectedCardImagePath = project.ImagePath,
            ExistingPictures = project.Pictures.ToList(),
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

        return View(model);
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
            var project = await _projectService.GetProjectByIdAsync(id);
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            model.ExistingPictures = project?.Pictures.ToList() ?? new();
            return View(model);
        }

        var existingProject = await _projectService.GetProjectByIdAsync(id);
        if (existingProject == null)
        {
            return NotFound();
        }

        existingProject.Slug = model.Slug;
        existingProject.GithubUrl = model.GithubUrl;
        existingProject.OrderIndex = model.OrderIndex;
        existingProject.UpdatedOn = DateTime.UtcNow;
        existingProject.Title = model.Titles.ToDictionary(t => t.LanguageCode, t => t.Text);
        existingProject.Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text);
        existingProject.ShortDescription = model.ShortDescriptions.ToDictionary(t => t.LanguageCode, t => t.Text);

        // Handle image deletions
        if (model.PicturesToDelete?.Any() == true)
        {
            foreach (var pictureId in model.PicturesToDelete)
            {
                var picture = await _pictureService.GetPictureByIdAsync(pictureId);
                if (picture != null)
                {
                    await DeleteUploadedImageAsync(picture.Source);
                    await _pictureService.DeletePictureAsync(pictureId);
                }
            }
        }

        // Handle new image uploads
        if (model.UploadedImages?.Any() == true)
        {
            foreach (var file in model.UploadedImages)
            {
                var imagePath = await SaveUploadedImageAsync(file);
                if (imagePath == null) continue;

                var picture = new Core.Models.Picture
                {
                    Source = imagePath,
                    ProjectId = existingProject.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };

                await _pictureService.CreatePictureAsync(picture);
            }
        }

        // Update card image
        existingProject.ImagePath = model.SelectedCardImagePath;

        await _projectService.UpdateProjectAsync(existingProject);

        TempData["SuccessMessage"] = "Project successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
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
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        // Delete all associated images
        foreach (var picture in project.Pictures)
        {
            await DeleteUploadedImageAsync(picture.Source);
            await _pictureService.DeletePictureAsync(picture.Id);
        }

        await _projectService.DeleteProjectAsync(id);

        TempData["SuccessMessage"] = "Project successfully deleted!";
        return RedirectToAction(nameof(Index));
    }

    // Helper methods
    private async Task<string?> SaveUploadedImageAsync(IFormFile file)
    {
        // Validate file type
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(fileExtension))
        {
            ModelState.AddModelError("UploadedImages", "Only image files (jpg, jpeg, png, gif, webp) are allowed.");
            return null;
        }

        // Validate file size (max 20MB)
        if (file.Length > 20 * 1024 * 1024)
        {
            ModelState.AddModelError("UploadedImages", "File size must not exceed 20MB.");
            return null;
        }

        // Create unique filename
        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploaded-pictures");

        // Ensure directory exists
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Save file
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return $"/images/uploaded-pictures/{uniqueFileName}";
    }

    private async Task DeleteUploadedImageAsync(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath) || !imagePath.StartsWith("/images/uploaded-pictures/"))
        {
            return;
        }

        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
        {
            await Task.Run(() => System.IO.File.Delete(filePath));
        }
    }
}