using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Web.Models;

namespace PersonalSite.Web.Controllers;

public class ContactController : Controller
{
    private readonly IContactFormService _contactFormService;

    public ContactController(IContactFormService contactFormService)
    {
        _contactFormService = contactFormService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new ContactViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var contact = new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Message = model.Message
            };

            await _contactFormService.SubmitContactFormAsync(contact);

            TempData["SuccessMessage"] = "Your message has been sent successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while sending your message. Please try again.");
            return View(model);
        }
    }
}
