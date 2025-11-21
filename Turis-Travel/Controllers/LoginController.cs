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

        // GET: /Login/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login/
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

            // Redirección según el rol
            if (u.IdRol == 1) // Administrador
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (u.IdRol == 2) // Empleado
            {
                return RedirectToAction("Index", "Empleado");
            }
            else if (u.IdRol == 3) // Cliente
            {
                return RedirectToAction("Index", "Cliente");
            }

            // Si no coincide ningún rol
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}


