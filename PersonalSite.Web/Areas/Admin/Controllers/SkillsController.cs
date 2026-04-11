using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillsController : Controller
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public async Task<IActionResult> Index()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return View(skills);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                await _skillService.CreateSkillAsync(skill);
                TempData["SuccessMessage"] = "Skill successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _skillService.UpdateSkillAsync(skill);
                TempData["SuccessMessage"] = "Skill successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

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
}