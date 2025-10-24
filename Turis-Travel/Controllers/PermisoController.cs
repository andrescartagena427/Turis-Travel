using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Turis_Travel.Models;
using System.Collections.Generic;

namespace Turis_Travel.Controllers
{
    public class PermisoController : Controller
    {
        private readonly ConexionBD conexion = new ConexionBD();

        // GET: Permiso
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
                {
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
            }

            // 👇 Esto es lo que hace que la vista tenga datos
            return View(permisos);
        }
    }
}
