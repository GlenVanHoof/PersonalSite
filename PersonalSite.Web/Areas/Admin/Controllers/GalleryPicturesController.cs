using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces.Services;
using PersonalSite.Web.Areas.Admin.Models;
using System.IO;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class GalleryPicturesController : Controller
{
    private readonly IGalleryPictureService _galleryPictureService;
    private readonly IPictureService _pictureService;

    public GalleryPicturesController(
        IGalleryPictureService galleryPictureService,
        IPictureService pictureService,
        IWebHostEnvironment webHostEnvironment)
    {
        _galleryPictureService = galleryPictureService;
        _pictureService = pictureService;
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
        if (!model.PictureId.HasValue)
        {
            ModelState.AddModelError("", "Please select an existing picture.");
            model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
            return View(model);
        }

        var galleryPicture = new Core.Models.GalleryPicture
        {
            PictureId = model.PictureId.Value
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

        var pictures = await _pictureService.GetAllPicturesAsync();

        var viewModel = new GalleryPictureEditViewModel
        {
            Id = galleryPicture.Id,
            PictureId = galleryPicture.PictureId,
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

        if (!model.PictureId.HasValue)
        {
            ModelState.AddModelError("", "Please select an existing picture.");
            model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
            return View(model);
        }

        var existingGalleryPicture = await _galleryPictureService.GetGalleryPictureByIdAsync(id);
        if (existingGalleryPicture == null)
        {
            return NotFound();
        }

        // Update gallery picture
        var galleryPicture = new Core.Models.GalleryPicture
        {
            Id = model.Id,
            PictureId = model.PictureId.Value,
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
        var allGalleryPictures = await _galleryPictureService.GetGalleryPicturesOrderedAsync();

        var isPictureStillInUse = allGalleryPictures.Any(gp => gp.PictureId == galleryPicture.PictureId && gp.Id != id) ||
                                  (picture != null && picture.ProjectId.HasValue);

        // If picture is not used elsewhere, delete it and its file
        if (!isPictureStillInUse && picture != null)
        {
            await _pictureService.DeletePictureAsync(picture.Id);
        }

        TempData["SuccessMessage"] = "Gallery picture successfully deleted!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Reorder(int pictureId, string direction, int amount = 1)
    {
        if (direction.ToLower() != "up" && direction.ToLower() != "down")
        {
            TempData["ErrorMessage"] = "Invalid reorder direction.";
            return RedirectToAction(nameof(Index));
        }
        await _galleryPictureService.ReorderGalleryPicturesAsync(pictureId, direction, amount);
        return RedirectToAction(nameof(Index));
    }
}