using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces.Services;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class ContactFormsController : Controller
{
    private readonly IContactFormService _contactFormService;

    public ContactFormsController(IContactFormService contactFormService)
    {
        _contactFormService = contactFormService;
    }

    public async Task<IActionResult> Index()
    {
        var contactForms = await _contactFormService.GetAllContactFormsAsync();
        return View(contactForms);
    }

    public async Task<IActionResult> Details(int id)
    {
        var contactForm = await _contactFormService.GetContactFormByIdAsync(id);
        if (contactForm == null)
        {
            return NotFound();
        }
        return View(contactForm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactFormService.DeleteContactFormAsync(id);
        TempData["SuccessMessage"] = "Contact form successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}