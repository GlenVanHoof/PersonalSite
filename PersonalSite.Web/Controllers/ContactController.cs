using Microsoft.AspNetCore.Mvc;

namespace PersoonlijkeSite.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
