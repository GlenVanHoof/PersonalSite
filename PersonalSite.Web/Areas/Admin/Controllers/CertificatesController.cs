using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Core.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificatesController : Controller
    {
        private readonly ICertificateService _certificateService;

        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public async Task<IActionResult> Index()
        {
            var certificates = await _certificateService.GetAllCertificatesAsync();
            return View(certificates);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                await _certificateService.CreateCertificateAsync(certificate);
                TempData["SuccessMessage"] = "Certificate successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(certificate);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }
            return View(certificate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Certificate certificate)
        {
            if (id != certificate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _certificateService.UpdateCertificateAsync(certificate);
                TempData["SuccessMessage"] = "Certificate successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(certificate);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var certificate = await _certificateService.GetCertificateByIdAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }
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
}