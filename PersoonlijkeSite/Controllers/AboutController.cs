using Microsoft.AspNetCore.Mvc;

namespace PersoonlijkeSite.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
