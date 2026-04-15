using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class GalleryPicturesController : Controller
{
    private readonly IGalleryPictureService _galleryPictureService;
    private readonly IPictureService _pictureService;

    public GalleryPicturesController(IGalleryPictureService galleryPictureService, IPictureService pictureService)
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
        if (!ModelState.IsValid)
        {
            model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
            return View(model);
        }

        var galleryPicture = new Core.Models.GalleryPicture
        {
            PictureId = model.PictureId,
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

        var pictures = await _pictureService.GetAllPicturesAsync();
        var viewModel = new GalleryPictureEditViewModel
        {
            Id = galleryPicture.Id,
            PictureId = galleryPicture.PictureId,
            Position = galleryPicture.Position,
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

        if (!ModelState.IsValid)
        {
            model.AvailablePictures = (await _pictureService.GetAllPicturesAsync()).ToList();
            return View(model);
        }

        var galleryPicture = new Core.Models.GalleryPicture
        {
            Id = model.Id,
            PictureId = model.PictureId,
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
        await _galleryPictureService.DeleteGalleryPictureAsync(id);
        TempData["SuccessMessage"] = "Gallery picture successfully removed!";
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