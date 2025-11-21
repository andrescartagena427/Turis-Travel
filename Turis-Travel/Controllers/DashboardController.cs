using Microsoft.AspNetCore.Mvc;

namespace Turis_Travel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("Id");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Nombre = HttpContext.Session.GetString("Nombre");
            ViewBag.Rol = HttpContext.Session.GetInt32("IdRol");
            return View();
        }

    }
}

