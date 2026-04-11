using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperiencesController : Controller
    {
        private readonly IExperienceService _experienceService;

        public ExperiencesController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        public async Task<IActionResult> Index()
        {
            var experiences = await _experienceService.GetAllExperiencesAsync();
            return View(experiences);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                await _experienceService.CreateExperienceAsync(experience);
                TempData["SuccessMessage"] = "Experience successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var experience = await _experienceService.GetExperienceByIdAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
            return View(experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Experience experience)
        {
            if (id != experience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _experienceService.UpdateExperienceAsync(experience);
                TempData["SuccessMessage"] = "Experience successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(experience);
        }

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
}