using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;
using PersonalSite.Web.Models;

namespace PersoonlijkeSite.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
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

            var contact = new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Message = model.Message,
                CreatedAt = DateTime.UtcNow
            };

            await _contactService.CreateContactAsync(contact);

            TempData["SuccessMessage"] = "Bedankt voor je bericht! Ik neem zo spoedig mogelijk contact met je op.";

            return RedirectToAction(nameof(Index));
        }
    }
}
