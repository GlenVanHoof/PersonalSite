using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectTranslationService _projectTranslationService;

        public ProjectsController(IProjectService projectService, IProjectTranslationService projectTranslationService)
        {
            _projectService = projectService;
            _projectTranslationService = projectTranslationService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var translations = await _projectTranslationService.GetTranslationsByProjectIdAsync(id);
            ViewBag.Translations = translations;
            return View(project);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.CreateProjectAsync(project);
                TempData["SuccessMessage"] = "Project successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _projectService.UpdateProjectAsync(project);
                TempData["SuccessMessage"] = "Project successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            TempData["SuccessMessage"] = "Project successfully deleted!";
            return RedirectToAction(nameof(Index));
        }

        // Translation Management Actions
        public async Task<IActionResult> CreateTranslation(int projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var translation = new ProjectTranslation { ProjectId = projectId };
            ViewBag.Project = project;
            return View(translation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTranslation(ProjectTranslation translation)
        {
            if (ModelState.IsValid)
            {
                await _projectTranslationService.CreateTranslationAsync(translation);
                TempData["SuccessMessage"] = "Translation successfully created!";
                return RedirectToAction(nameof(Details), new { id = translation.ProjectId });
            }

            var project = await _projectService.GetProjectByIdAsync(translation.ProjectId);
            ViewBag.Project = project;
            return View(translation);
        }

        public async Task<IActionResult> EditTranslation(int id)
        {
            var translation = await _projectTranslationService.GetTranslationByIdAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetProjectByIdAsync(translation.ProjectId);
            ViewBag.Project = project;
            return View(translation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTranslation(int id, ProjectTranslation translation)
        {
            if (id != translation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _projectTranslationService.UpdateTranslationAsync(translation);
                TempData["SuccessMessage"] = "Translation successfully updated!";
                return RedirectToAction(nameof(Details), new { id = translation.ProjectId });
            }

            var project = await _projectService.GetProjectByIdAsync(translation.ProjectId);
            ViewBag.Project = project;
            return View(translation);
        }

        public async Task<IActionResult> DeleteTranslation(int id)
        {
            var translation = await _projectTranslationService.GetTranslationByIdAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetProjectByIdAsync(translation.ProjectId);
            ViewBag.Project = project;
            return View(translation);
        }

        [HttpPost, ActionName("DeleteTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTranslationConfirmed(int id)
        {
            var translation = await _projectTranslationService.GetTranslationByIdAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            var projectId = translation.ProjectId;
            await _projectTranslationService.DeleteTranslationAsync(id);
            TempData["SuccessMessage"] = "Translation successfully deleted!";
            return RedirectToAction(nameof(Details), new { id = projectId });
        }
    }
}