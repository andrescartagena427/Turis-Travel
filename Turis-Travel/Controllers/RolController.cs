using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class RolController : Controller
    {
        // Simulación de datos (puedes reemplazar por base de datos más adelante)
        private static List<Rol> roles = new List<Rol>
        {
            new Rol { ID_rol = 1, Nombre_rol = "Administrador", Estado_rol = 1 },
            new Rol { ID_rol = 2, Nombre_rol = "Empleado", Estado_rol = 1 },
            new Rol { ID_rol = 3, Nombre_rol = "Cliente", Estado_rol = 0 }
        };

        public IActionResult Index()
        {
            return View(roles);
        }

        // ======================
        // CREAR NUEVO ROL
        // ======================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rol nuevoRol)
        {
            if (ModelState.IsValid)
            {
                // Simular autoincremento
                int nuevoId = roles.Any() ? roles.Max(r => r.ID_rol) + 1 : 1;
                nuevoRol.ID_rol = nuevoId;
                roles.Add(nuevoRol);
                return RedirectToAction(nameof(Index));
            }
            return View(nuevoRol);
        }

        // ======================
        // EDITAR ROL EXISTENTE
        // ======================
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rol = roles.FirstOrDefault(r => r.ID_rol == id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rol rolEditado)
        {
            var rol = roles.FirstOrDefault(r => r.ID_rol == rolEditado.ID_rol);
            if (rol == null)
            {
                return NotFound();
            }

            rol.Nombre_rol = rolEditado.Nombre_rol;
            rol.Estado_rol = rolEditado.Estado_rol;

            return RedirectToAction(nameof(Index));
        }

        // ======================
        // ELIMINAR ROL
        // ======================
        public IActionResult Delete(int id)
        {
            var rol = roles.FirstOrDefault(r => r.ID_rol == id);
            if (rol != null)
            {
                roles.Remove(rol);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
