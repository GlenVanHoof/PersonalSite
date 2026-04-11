using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;

namespace PersonalSite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            // Sorteer op CreatedAt, recentste eerst
            var sortedContacts = contacts.OrderByDescending(c => c.CreatedAt);
            return View(sortedContacts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactService.DeleteContactAsync(id);
            TempData["SuccessMessage"] = "Contactbericht succesvol verwijderd!";
            return RedirectToAction(nameof(Index));
        }
    }
}