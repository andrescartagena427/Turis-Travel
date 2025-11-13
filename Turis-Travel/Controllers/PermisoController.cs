using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Turis_Travel.Models;
using System.Collections.Generic;

namespace Turis_Travel.Controllers
{
    public class PermisoController : Controller
    {
        private readonly ConexionBD conexion = new ConexionBD();

        // ✅ LISTAR
        public IActionResult Index()
        {
            List<Permiso> permisos = new List<Permiso>();

            using (var con = conexion.GetConnection())
            {
                con.Open();
                string query = @"SELECT p.ID_permiso, p.ID_rol, p.ID_modulo, p.Estado_permiso,
                                        r.Nombre_rol, m.Nombre_modulo
                                 FROM Permisos p
                                 JOIN Roles r ON p.ID_rol = r.ID_rol
                                 JOIN Modulos m ON p.ID_modulo = m.ID_modulo;";

                using (var cmd = new MySqlCommand(query, con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        permisos.Add(new Permiso
                        {
                            ID_permiso = reader.GetInt32("ID_permiso"),
                            ID_rol = reader.GetInt32("ID_rol"),
                            ID_modulo = reader.GetInt32("ID_modulo"),
                            Estado_permiso = reader.GetInt32("Estado_permiso"),
                            Nombre_rol = reader.GetString("Nombre_rol"),
                            Nombre_modulo = reader.GetString("Nombre_modulo")
                        });
                    }
                }
            }

            return View(permisos);
        }

        // ✅ CREAR
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                using (var con = conexion.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO Permisos (ID_rol, ID_modulo, Estado_permiso) VALUES (@ID_rol, @ID_modulo, @Estado_permiso)";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID_rol", permiso.ID_rol);
                        cmd.Parameters.AddWithValue("@ID_modulo", permiso.ID_modulo);
                        cmd.Parameters.AddWithValue("@Estado_permiso", permiso.Estado_permiso);
                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(permiso);
        }

        //  EDITAR
        public IActionResult Edit(int id)
        {
            Permiso permiso = null;

            using (var con = conexion.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM Permisos WHERE ID_permiso = @ID";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            permiso = new Permiso
                            {
                                ID_permiso = reader.GetInt32("ID_permiso"),
                                ID_rol = reader.GetInt32("ID_rol"),
                                ID_modulo = reader.GetInt32("ID_modulo"),
                                Estado_permiso = reader.GetInt32("Estado_permiso")
                            };
                        }
                    }
                }
            }

            if (permiso == null)
                return NotFound();

            return View(permiso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                using (var con = conexion.GetConnection())
                {
                    con.Open();
                    string query = "UPDATE Permisos SET ID_rol = @ID_rol, ID_modulo = @ID_modulo, Estado_permiso = @Estado_permiso WHERE ID_permiso = @ID_permiso";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID_rol", permiso.ID_rol);
                        cmd.Parameters.AddWithValue("@ID_modulo", permiso.ID_modulo);
                        cmd.Parameters.AddWithValue("@Estado_permiso", permiso.Estado_permiso);
                        cmd.Parameters.AddWithValue("@ID_permiso", permiso.ID_permiso);
                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(permiso);
        }

        //  ELIMINAR
        public IActionResult Delete(int id)
        {
            Permiso permiso = null;

            using (var con = conexion.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM Permisos WHERE ID_permiso = @ID";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            permiso = new Permiso
                            {
                                ID_permiso = reader.GetInt32("ID_permiso"),
                                ID_rol = reader.GetInt32("ID_rol"),
                                ID_modulo = reader.GetInt32("ID_modulo"),
                                Estado_permiso = reader.GetInt32("Estado_permiso")
                            };
                        }
                    }
                }
            }

            if (permiso == null)
                return NotFound();

            return View(permiso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var con = conexion.GetConnection())
            {
                con.Open();
                string query = "DELETE FROM Permisos WHERE ID_permiso = @ID";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
