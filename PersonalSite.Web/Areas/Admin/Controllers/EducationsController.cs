using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class EducationsController : Controller
{
    private readonly IEducationService _educationService;
    private readonly ILanguageService _languageService;

    public EducationsController(IEducationService educationService, ILanguageService languageService)
    {
        _educationService = educationService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var educations = await _educationService.GetEducationsOrderedByDateAsync();
        return View(educations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var education = await _educationService.GetEducationByIdAsync(id);
        if (education == null)
        {
            return NotFound();
        }
        return View(education);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new EducationEditViewModel
        {
            Languages = languages.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EducationEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var education = new Core.Models.Education
        {
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Institution = model.Institutions.ToDictionary(t => t.LanguageCode, t => t.Text),
            Degree = model.Degrees.ToDictionary(t => t.LanguageCode, t => t.Text),
            FieldOfStudy = model.FieldsOfStudy.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _educationService.CreateEducationAsync(education);
        TempData["SuccessMessage"] = "Education successfully created!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var education = await _educationService.GetEducationByIdAsync(id);
        if (education == null)
        {
            return NotFound();
        }

        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new EducationEditViewModel
        {
            Id = education.Id,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            Languages = languages.ToList(),
            Institutions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = education.Institution.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            Degrees = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = education.Degree.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            FieldsOfStudy = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = education.FieldOfStudy?.GetValueOrDefault(l.Code, string.Empty) ?? string.Empty
            }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = education.Description?.GetValueOrDefault(l.Code, string.Empty) ?? string.Empty
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EducationEditViewModel model)
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

        var education = new Core.Models.Education
        {
            Id = model.Id,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Institution = model.Institutions.ToDictionary(t => t.LanguageCode, t => t.Text),
            Degree = model.Degrees.ToDictionary(t => t.LanguageCode, t => t.Text),
            FieldOfStudy = model.FieldsOfStudy.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _educationService.UpdateEducationAsync(education);
        TempData["SuccessMessage"] = "Education successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var education = await _educationService.GetEducationByIdAsync(id);
        if (education == null)
        {
            return NotFound();
        }
        return View(education);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _educationService.DeleteEducationAsync(id);
        TempData["SuccessMessage"] = "Education successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}