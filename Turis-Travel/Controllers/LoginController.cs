using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioBD _usuarioBD;

        public LoginController()
        {
            _usuarioBD = new UsuarioBD();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string correo, string clave)
        {
            Usuario u = _usuarioBD.ValidarLogin(correo, clave);

            if (u == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos";
                return View();
            }

            // Guardar sesión
            HttpContext.Session.SetInt32("Id", u.Id);
            HttpContext.Session.SetString("Nombre", u.Nombre);
            HttpContext.Session.SetInt32("IdRol", u.IdRol);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
