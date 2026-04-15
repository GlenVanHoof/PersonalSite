using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class CertificatesController : Controller
{
    private readonly ICertificateService _certificateService;
    private readonly ILanguageService _languageService;

    public CertificatesController(ICertificateService certificateService, ILanguageService languageService)
    {
        _certificateService = certificateService;
        _languageService = languageService;
    }

    public async Task<IActionResult> Index()
    {
        var certificates = await _certificateService.GetAllCertificatesAsync();
        return View(certificates);
    }

    public async Task<IActionResult> Create()
    {
        var languages = await _languageService.GetAllLanguagesAsync();
        var model = new CertificateEditViewModel
        {
            Languages = languages.ToList(),
            Names = languages.Select(l => new TranslationInputViewModel { LanguageCode = l.Code, LanguageName = l.Name }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel { LanguageCode = l.Code, LanguageName = l.Name }).ToList()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CertificateEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var certificate = new Core.Models.Certificate
        {
            Organisation = model.Organisation ?? string.Empty,
            AcquiredOn = model.AcquiredOn ?? DateTime.UtcNow,
            Name = model.Names.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _certificateService.CreateCertificateAsync(certificate);
        TempData["SuccessMessage"] = "Certificate successfully created!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var certificate = await _certificateService.GetCertificateByIdAsync(id);
        if (certificate == null)
            return NotFound();

        var languages = await _languageService.GetAllLanguagesAsync();
        var model = new CertificateEditViewModel
        {
            Id = certificate.Id,
            Organisation = certificate.Organisation,
            AcquiredOn = certificate.AcquiredOn,
            Languages = languages.ToList(),
            Names = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = certificate.Name.TryGetValue(l.Code, out var name) ? name : string.Empty
            }).ToList(),
            Descriptions = languages.Select(l => new TranslationInputViewModel
            {
                LanguageCode = l.Code,
                LanguageName = l.Name,
                Text = certificate.Description.TryGetValue(l.Code, out var desc) ? desc : string.Empty
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CertificateEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Languages = (await _languageService.GetAllLanguagesAsync()).ToList();
            return View(model);
        }

        var certificate = new Core.Models.Certificate
        {
            Id = model.Id,
            Organisation = model.Organisation ?? string.Empty,
            AcquiredOn = model.AcquiredOn ?? DateTime.UtcNow,
            Name = model.Names.ToDictionary(t => t.LanguageCode, t => t.Text),
            Description = model.Descriptions.ToDictionary(t => t.LanguageCode, t => t.Text)
        };

        await _certificateService.UpdateCertificateAsync(certificate);
        TempData["SuccessMessage"] = "Certificate successfully updated!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var certificate = await _certificateService.GetCertificateByIdAsync(id);
        if (certificate == null)
            return NotFound();

        return View(certificate);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var certificate = await _certificateService.GetCertificateByIdAsync(id);
        if (certificate == null)
            return NotFound();

        return View(certificate);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _certificateService.DeleteCertificateAsync(id);
        TempData["SuccessMessage"] = "Certificate successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}