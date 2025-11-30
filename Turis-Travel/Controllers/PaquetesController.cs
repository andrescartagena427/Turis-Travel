using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class PaqueteController : Controller
    {
        private readonly ConexionBD _conexion;

        public PaqueteController()
        {
            _conexion = new ConexionBD();
        }

        // =============== LISTA ===============
        public IActionResult Index()
        {
            var lista = new List<Paquete>();

            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Paquetes_Turisticos", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Paquete
                    {
                        ID_paquete = reader.GetInt32("ID_paquete"),
                        Nombre_paquete = reader.GetString("Nombre_paquete"),
                        Descripcion = reader.GetString("Descripcion"),
                        Precio_base = reader.GetDecimal("Precio_base"),
                        Fecha_inicio = reader.IsDBNull("Fecha_inicio") ? null : reader.GetDateTime("Fecha_inicio"),
                        Fecha_fin = reader.IsDBNull("Fecha_fin") ? null : reader.GetDateTime("Fecha_fin"),
                        Capacidad_maxima = reader.IsDBNull("Capacidad_maxima") ? null : reader.GetInt32("Capacidad_maxima"),
                        Estado = reader.GetString("Estado")
                    });
                }
            }

            return View(lista);
        }

        // =============== CREAR: VISTA ===============
        public IActionResult Crear()
        {
            return View();
        }

        // =============== CREAR: POST ===============
        [HttpPost]
        public IActionResult Crear(Paquete p)
        {
            if (!ModelState.IsValid)
                return View(p);

            using (var conn = _conexion.GetConnection())
            {
                conn.Open();

                var cmd = new MySqlCommand(
                    @"INSERT INTO Paquetes_Turisticos 
                    (Nombre_paquete, Descripcion, Precio_base, Fecha_inicio, Fecha_fin, Capacidad_maxima, Estado)
                    VALUES (@Nom, @Desc, @Precio, @Inicio, @Fin, @Cap, @Estado)", conn);

                cmd.Parameters.AddWithValue("@Nom", p.Nombre_paquete);
                cmd.Parameters.AddWithValue("@Desc", p.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", p.Precio_base);
                cmd.Parameters.AddWithValue("@Inicio", p.Fecha_inicio);
                cmd.Parameters.AddWithValue("@Fin", p.Fecha_fin);
                cmd.Parameters.AddWithValue("@Cap", p.Capacidad_maxima);
                cmd.Parameters.AddWithValue("@Estado", p.Estado);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}

