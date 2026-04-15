using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class GalleryPicturesController : Controller
{
    private readonly IGalleryPictureService _galleryPictureService;
    private readonly IPictureService _pictureService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GalleryPicturesController(
        IGalleryPictureService galleryPictureService, 
        IPictureService pictureService,
        IWebHostEnvironment webHostEnvironment)
    {
        _galleryPictureService = galleryPictureService;
        _pictureService = pictureService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var galleryPictures = await _galleryPictureService.GetGalleryPicturesOrderedAsync();
        return View(galleryPictures);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var pictures = await _pictureService.GetAllPicturesAsync();
        var viewModel = new GalleryPictureEditViewModel
        {
            AvailablePictures = pictures.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GalleryPictureEditViewModel model)
    {
        // Validate input based on mode
        if (model.UseExistingPicture)
        {
            if (!model.PictureId.HasValue)
            {
                ModelState.AddModelError("", "Please select an existing picture.");
                model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
                return View(model);
            }
        }
        else
        {
            if (model.UploadedFile == null && string.IsNullOrWhiteSpace(model.Source))
            {
                ModelState.AddModelError("", "Please either upload an image or provide an image URL.");
                model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
                return View(model);
            }
        }

        int pictureId;

        if (model.UseExistingPicture && model.PictureId.HasValue)
        {
            // Use existing picture
            pictureId = model.PictureId.Value;
        }
        else
        {
            // Create new picture
            string imagePath;

            if (model.UploadedFile != null)
            {
                // Validate file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(model.UploadedFile.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("UploadedFile", "Only image files (jpg, jpeg, png, gif, webp) are allowed.");
                    model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
                    return View(model);
                }

                // Validate file size (max 5MB)
                if (model.UploadedFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UploadedFile", "File size must not exceed 5MB.");
                    model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
                    return View(model);
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
                    await model.UploadedFile.CopyToAsync(fileStream);
                }

                imagePath = $"/images/uploaded-pictures/{uniqueFileName}";
            }
            else
            {
                // Use provided URL
                imagePath = model.Source!;
            }

            // Create picture record
            var picture = new Core.Models.Picture
            {
                Source = imagePath,
                ProjectId = null // Gallery pictures don't have projects
            };

            var createdPicture = await _pictureService.CreatePictureAsync(picture);
            pictureId = createdPicture.Id;
        }

        // Create gallery picture
        var galleryPicture = new Core.Models.GalleryPicture
        {
            PictureId = pictureId,
            Position = model.Position
        };

        await _galleryPictureService.CreateGalleryPictureAsync(galleryPicture);
        TempData["SuccessMessage"] = "Gallery picture successfully added!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var galleryPicture = await _galleryPictureService.GetGalleryPictureByIdAsync(id);
        if (galleryPicture == null)
        {
            return NotFound();
        }

        var picture = await _pictureService.GetPictureByIdAsync(galleryPicture.PictureId);
        var pictures = await _pictureService.GetAllPicturesAsync();
        
        var viewModel = new GalleryPictureEditViewModel
        {
            Id = galleryPicture.Id,
            PictureId = galleryPicture.PictureId,
            Position = galleryPicture.Position,
            ExistingSource = picture?.Source,
            AvailablePictures = pictures.ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GalleryPictureEditViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        var existingGalleryPicture = await _galleryPictureService.GetGalleryPictureByIdAsync(id);
        if (existingGalleryPicture == null)
        {
            return NotFound();
        }

        int pictureId = existingGalleryPicture.PictureId;

        // Check if user wants to change the picture
        if (model.UseExistingPicture && model.PictureId.HasValue && model.PictureId.Value != existingGalleryPicture.PictureId)
        {
            // Switch to existing picture
            pictureId = model.PictureId.Value;
        }
        else if (!model.UseExistingPicture && (model.UploadedFile != null || !string.IsNullOrWhiteSpace(model.Source)))
        {
            // Upload new picture or change URL
            var existingPicture = await _pictureService.GetPictureByIdAsync(existingGalleryPicture.PictureId);
            string imagePath = existingPicture?.Source ?? string.Empty;

            if (model.UploadedFile != null)
            {
                // Validate file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(model.UploadedFile.FileName).ToLowerInvariant();
                
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("UploadedFile", "Only image files (jpg, jpeg, png, gif, webp) are allowed.");
                    model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
                    model.ExistingSource = existingPicture?.Source;
                    return View(model);
                }

                // Validate file size (max 5MB)
                if (model.UploadedFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("UploadedFile", "File size must not exceed 5MB.");
                    model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
                    model.ExistingSource = existingPicture?.Source;
                    return View(model);
                }

                // Delete old file if it's an uploaded file
                if (existingPicture != null && existingPicture.Source.StartsWith("/images/uploaded-pictures/"))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingPicture.Source.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
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
                    await model.UploadedFile.CopyToAsync(fileStream);
                }

                imagePath = $"/images/uploaded-pictures/{uniqueFileName}";
            }
            else if (!string.IsNullOrWhiteSpace(model.Source))
            {
                imagePath = model.Source;
            }

            // Update existing picture
            if (existingPicture != null)
            {
                existingPicture.Source = imagePath;
                await _pictureService.UpdatePictureAsync(existingPicture);
            }
        }

        // Update gallery picture
        var galleryPicture = new Core.Models.GalleryPicture
        {
            Id = model.Id,
            PictureId = pictureId,
            Position = model.Position
        };

        await _galleryPictureService.UpdateGalleryPictureAsync(galleryPicture);
        TempData["SuccessMessage"] = "Gallery picture successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var galleryPicture = await _galleryPictureService.GetGalleryPictureByIdAsync(id);
        if (galleryPicture == null)
        {
            return NotFound();
        }
        return View(galleryPicture);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var galleryPicture = await _galleryPictureService.GetGalleryPictureByIdAsync(id);
        if (galleryPicture == null)
        {
            return NotFound();
        }

        // Get associated picture
        var picture = await _pictureService.GetPictureByIdAsync(galleryPicture.PictureId);
        
        // Delete gallery picture first
        await _galleryPictureService.DeleteGalleryPictureAsync(id);

        // Check if this picture is used by other gallery pictures or projects
        var allPictures = await _pictureService.GetAllPicturesAsync();
        var allGalleryPictures = await _galleryPictureService.GetGalleryPicturesOrderedAsync();
        
        var isPictureStillInUse = allGalleryPictures.Any(gp => gp.PictureId == galleryPicture.PictureId && gp.Id != id) ||
                                  (picture != null && picture.ProjectId.HasValue);

        // If picture is not used elsewhere, delete it and its file
        if (!isPictureStillInUse && picture != null)
        {
            // Delete physical file if it's an uploaded file
            if (picture.Source.StartsWith("/images/uploaded-pictures/"))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, picture.Source.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            await _pictureService.DeletePictureAsync(picture.Id);
        }

        TempData["SuccessMessage"] = "Gallery picture successfully deleted!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reorder(List<int> positions)
    {
        await _galleryPictureService.ReorderGalleryPicturesAsync(positions);
        return Json(new { success = true });
    }
}