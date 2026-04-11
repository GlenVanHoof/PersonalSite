using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationsController : Controller
    {
        private readonly IEducationService _educationService;

        public EducationsController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public async Task<IActionResult> Index()
        {
            var educations = await _educationService.GetAllEducationsAsync();
            return View(educations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Education education)
        {
            if (ModelState.IsValid)
            {
                await _educationService.AddEducationAsync(education);
                TempData["SuccessMessage"] = "Education successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var education = await _educationService.GetEducationByIdAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Education education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _educationService.UpdateEducationAsync(education);
                TempData["SuccessMessage"] = "Education successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(education);
        }

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
}