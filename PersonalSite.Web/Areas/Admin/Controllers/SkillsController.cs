using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class SkillsController : Controller
{
    private readonly ISkillService _skillService;
    private readonly ILanguageService _languageService;

    public SkillsController(ISkillService skillService, ILanguageService languageService)
    {
        _skillService = skillService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var skills = await _skillService.GetSkillsOrderedByScoreAsync();
        return View(skills);
    }

    public async Task<IActionResult> Details(int id)
    {
        var skill = await _skillService.GetSkillByIdAsync(id);
        if (skill == null)
        {
            return NotFound();
        }
        return View(skill);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new SkillEditViewModel
        {
            Languages = languages.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SkillEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var skill = new Core.Models.Skill
        {
            Type = model.Type,
            ScoreOutOf100 = model.ScoreOutOf100,
            Name = model.Names.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _skillService.CreateSkillAsync(skill);
        TempData["SuccessMessage"] = "Skill successfully created!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var skill = await _skillService.GetSkillByIdAsync(id);
        if (skill == null)
        {
            return NotFound();
        }

        var languages = await _languageService.GetAllLanguagesAsync();
        var viewModel = new SkillEditViewModel
        {
            Id = skill.Id,
            Type = skill.Type,
            ScoreOutOf100 = skill.ScoreOutOf100,
            Languages = languages.ToList(),
            Names = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = skill.Name.GetValueOrDefault(l.Code, string.Empty)
            }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = skill.Description?.GetValueOrDefault(l.Code, string.Empty) ?? string.Empty
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SkillEditViewModel model)
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

        var skill = new Core.Models.Skill
        {
            Id = model.Id,
            Type = model.Type,
            ScoreOutOf100 = model.ScoreOutOf100,
            Name = model.Names.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _skillService.UpdateSkillAsync(skill);
        TempData["SuccessMessage"] = "Skill successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var skill = await _skillService.GetSkillByIdAsync(id);
        if (skill == null)
        {
            return NotFound();
        }
        return View(skill);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _skillService.DeleteSkillAsync(id);
        TempData["SuccessMessage"] = "Skill successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}