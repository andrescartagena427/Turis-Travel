using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Turis_Travel.Models;
using System.Collections.Generic;

namespace Turis_Travel.Controllers
{
    public class RolController : Controller
    {
        private readonly ConexionBD conexion = new ConexionBD();

        // Mostrar lista de roles
        public IActionResult Index()
        {
            List<Rol> roles = new List<Rol>();
            using (var con = conexion.GetConnection())
            {
                con.Open();
                string query = "SELECT ID_rol, Nombre_rol, Estado_rol FROM Roles";
                using (var cmd = new MySqlCommand(query, con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Rol
                            {
                                ID_rol = reader.GetInt32("ID_rol"),
                                Nombre_rol = reader.GetString("Nombre_rol"),
                                Estado_rol = reader.GetInt32("Estado_rol")
                            });
                        }
                    }
                }
            }
            return View(roles);
        }

        // Crear nuevo rol (GET)
        public IActionResult Crear()
        {
            return View();
        }

        // Crear nuevo rol (POST)
        [HttpPost]
        public IActionResult Crear(Rol rol)
        {
            if (ModelState.IsValid)
            {
                using (var con = conexion.GetConnection())
                {
                    con.Open();
                    string query = "INSERT INTO Roles (Nombre_rol, Estado_rol) VALUES (@nombre, @estado)";
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nombre", rol.Nombre_rol);
                        cmd.Parameters.AddWithValue("@estado", rol.Estado_rol);
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        // Eliminar rol
        public IActionResult Eliminar(int id)
        {
            using (var con = conexion.GetConnection())
            {
                con.Open();
                string query = "DELETE FROM Roles WHERE ID_rol = @id";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
