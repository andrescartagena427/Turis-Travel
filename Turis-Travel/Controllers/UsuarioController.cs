using Microsoft.AspNetCore.Mvc;
using Turis_Travel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Turis_Travel.Controllers
{
    public class UsuarioController : Controller
    {
        // Datos simulados (puedes reemplazar luego por base de datos)
        private static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Carlos Gómez", Correo = "carlos@turis.com", Rol = "Administrador", Activo = true },
            new Usuario { Id = 2, Nombre = "María López", Correo = "maria@turis.com", Rol = "Empleado", Activo = true },
            new Usuario { Id = 3, Nombre = "Juan Pérez", Correo = "juan@turis.com", Rol = "Cliente", Activo = false }
        };

        // GET: /Usuario/
        public IActionResult Index()
        {
            return View(usuarios);
        }

        // GET: /Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Usuario/Create
        [HttpPost]
        public IActionResult Create(Usuario nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                nuevoUsuario.Id = usuarios.Count + 1;
                usuarios.Add(nuevoUsuario);
                return RedirectToAction("Index");
            }
            return View(nuevoUsuario);
        }

        // GET: /Usuario/Edit/1
        public IActionResult Edit(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();
            return View(usuario);
        }

        // POST: /Usuario/Edit
        [HttpPost]
        public IActionResult Edit(Usuario usuarioEditado)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == usuarioEditado.Id);
            if (usuario == null)
                return NotFound();

            usuario.Nombre = usuarioEditado.Nombre;
            usuario.Correo = usuarioEditado.Correo;
            usuario.Rol = usuarioEditado.Rol;
            usuario.Activo = usuarioEditado.Activo;

            return RedirectToAction("Index");
        }

        // GET: /Usuario/Delete/1
        public IActionResult Delete(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
                usuarios.Remove(usuario);

            return RedirectToAction("Index");
        }
    }
}

