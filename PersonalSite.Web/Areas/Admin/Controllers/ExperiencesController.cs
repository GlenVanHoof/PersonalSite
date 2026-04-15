using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class ExperiencesController : Controller
{
    private readonly IExperienceService _experienceService;
    private readonly ILanguageService _languageService;

    public ExperiencesController(IExperienceService experienceService, ILanguageService languageService)
    {
        _experienceService = experienceService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var experiences = await _experienceService.GetExperiencesOrderedByDateAsync();
        return View(experiences);
    }

    public async Task<IActionResult> Details(int id)
    {
        var experience = await _experienceService.GetExperienceByIdAsync(id);
        if (experience == null)
        {
            return NotFound();
        }
        return View(experience);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new ExperienceEditViewModel
        {
            Languages = languages.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExperienceEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var experience = new Core.Models.Experience
        {
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Company = model.Companies.ToDictionary(t => t.LanguageCode, t => t.Text),
            Position = model.Positions.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _experienceService.CreateExperienceAsync(experience);
        TempData["SuccessMessage"] = "Experience successfully created!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var experience = await _experienceService.GetExperienceByIdAsync(id);
        if (experience == null)
        {
            return NotFound();
        }

        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new ExperienceEditViewModel
        {
            Id = experience.Id,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Languages = languages.ToList(),
            Companies = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = experience.Company.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            Positions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = experience.Position.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = experience.Description?.GetValueOrDefault(l.Code, string.Empty) ?? string.Empty
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ExperienceEditViewModel model)
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

        var experience = new Core.Models.Experience
        {
            Id = model.Id,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Company = model.Companies.ToDictionary(t => t.LanguageCode, t => t.Text),
            Position = model.Positions.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _experienceService.UpdateExperienceAsync(experience);
        TempData["SuccessMessage"] = "Experience successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var experience = await _experienceService.GetExperienceByIdAsync(id);
        if (experience == null)
        {
            return NotFound();
        }
        return View(experience);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _experienceService.DeleteExperienceAsync(id);
        TempData["SuccessMessage"] = "Experience successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}