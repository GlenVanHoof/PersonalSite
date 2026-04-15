using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class PicturesController : Controller
{
    private readonly IPictureService _pictureService;
    private readonly IProjectService _projectService;

    public PicturesController(IPictureService pictureService, IProjectService projectService)
    {
        _pictureService = pictureService;
        _projectService = projectService;
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
        if (!ModelState.IsValid)
        {
            model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
            return View(model);
        }

        var picture = new Core.Models.Picture
        {
            Source = model.Source,
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
            Source = picture.Source,
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

        if (!ModelState.IsValid)
        {
            model.AvailableProjects = (await _projectService.GetAllProjectsAsync()).ToList();
            return View(model);
        }

        var picture = new Core.Models.Picture
        {
            Id = model.Id,
            Source = model.Source,
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