using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Web.Areas.Admin.Models;
using System.IO;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class PicturesController : Controller
{
    private readonly IPictureService _pictureService;
    private readonly IProjectService _projectService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PicturesController(
        IPictureService pictureService,
        IProjectService projectService,
        IWebHostEnvironment webHostEnvironment)
    {
        _pictureService = pictureService;
        _projectService = projectService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var pictures = await _pictureService.GetAllPicturesAsync();
        return View(pictures);
    }

    public async Task<IActionResult> Details(int id)
    {
        var picture = await _pictureService.GetPictureByIdAsync(id);
        if (picture == null)
        {
            return NotFound();
        }
        return View(picture);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        var viewModel = new PictureEditViewModel
        {
            AvailableProjects = projects.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PictureEditViewModel model)
    {
        // Validate that either file upload or URL is provided
        if (model.UploadedFile == null && string.IsNullOrWhiteSpace(model.Source))
        {
            ModelState.AddModelError("", "Please either upload an image or provide an image URL.");
            model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
            return View(model);
        }

        string imagePath;

        // Handle file upload
        if (model.UploadedFile != null)
        {
            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(model.UploadedFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("UploadedFile", "Only image files (jpg, jpeg, png, gif, webp) are allowed.");
                model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
                return View(model);
            }

            // Validate file size (max 20MB)
            if (model.UploadedFile.Length > 20 * 1024 * 1024)
            {
                ModelState.AddModelError("UploadedFile", "File size must not exceed 20MB.");
                model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
                return View(model);
            }

            // Create unique filename
            //var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var FileName = model.UploadedFile.FileName;
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploaded-pictures");

            // Ensure directory exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, FileName);

            // Save file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.UploadedFile.CopyToAsync(fileStream);
            }

            imagePath = $"/images/uploaded-pictures/{FileName}";
        }
        else
        {
            // Use provided URL
            imagePath = model.Source!;
        }

        var picture = new Core.Models.Picture
        {
            Source = imagePath,
            ProjectId = model.ProjectId
        };

        await _pictureService.CreatePictureAsync(picture);
        TempData["SuccessMessage"] = "Picture successfully created!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var picture = await _pictureService.GetPictureByIdAsync(id);
        if (picture == null)
        {
            return NotFound();
        }

        var projects = await _projectService.GetAllProjectsAsync();
        var viewModel = new PictureEditViewModel
        {
            Id = picture.Id,
            ExistingSource = picture.Source,
            ProjectId = picture.ProjectId,
            AvailableProjects = projects.ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PictureEditViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        var existingPicture = await _pictureService.GetPictureByIdAsync(id);
        if (existingPicture == null)
        {
            return NotFound();
        }

        string imagePath = existingPicture.Source;

        // Handle file upload (if new file is uploaded)
        if (model.UploadedFile != null)
        {
            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(model.UploadedFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("UploadedFile", "Only image files (jpg, jpeg, png, gif, webp) are allowed.");
                model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
                model.ExistingSource = existingPicture.Source;
                return View(model);
            }

            // Validate file size (max 20MB)
            if (model.UploadedFile.Length > 20 * 1024 * 1024)
            {
                ModelState.AddModelError("UploadedFile", "File size must not exceed 20MB.");
                model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
                model.ExistingSource = existingPicture.Source;
                return View(model);
            }

            // Delete old file if it's an uploaded file (not external URL)
            if (existingPicture.Source.StartsWith("/images/uploaded-pictures/"))
            {
                var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingPicture.Source.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            // Create unique filename
            var fileName = model.UploadedFile.FileName;
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploaded-pictures");

            // Ensure directory exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, fileName);

            // Save file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.UploadedFile.CopyToAsync(fileStream);
            }

            imagePath = $"/images/uploaded-pictures/{fileName}";
        }
        else if (!string.IsNullOrWhiteSpace(model.Source))
        {
            // Use new URL if provided
            imagePath = model.Source;
        }

        var picture = new Core.Models.Picture
        {
            Id = model.Id,
            Source = imagePath,
            ProjectId = model.ProjectId
        };

        await _pictureService.UpdatePictureAsync(picture);
        TempData["SuccessMessage"] = "Picture successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var picture = await _pictureService.GetPictureByIdAsync(id);
        if (picture == null)
        {
            return NotFound();
        }
        return View(picture);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var picture = await _pictureService.GetPictureByIdAsync(id);
        if (picture == null)
        {
            return NotFound();
        }

        // Delete physical file if it's an uploaded file
        if (picture.Source.StartsWith("/images/uploaded-pictures/"))
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, picture.Source.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        await _pictureService.DeletePictureAsync(id);
        TempData["SuccessMessage"] = "Picture successfully deleted!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ByProject(int projectId)
    {
        var pictures = await _pictureService.GetPicturesByProjectIdAsync(projectId);
        ViewBag.ProjectId = projectId;
        return View("Index", pictures);
    }
}