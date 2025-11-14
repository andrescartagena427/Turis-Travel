using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly ConexionBD _conexion;

        public PaquetesController()
        {
            _conexion = new ConexionBD();
        }

        // ==================================================
        // MÉTODO REUTILIZABLE PARA OBTENER UN PAQUETE
        // ==================================================
        private Paquetes ObtenerPaquete(int id)
        {
            Paquetes paquete = null;

            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Paquetes_Turisticos WHERE ID_paquete = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    paquete = new Paquetes
                    {
                        ID_paquete = reader.GetInt32("ID_paquete"),
                        Nombre_paquete = reader.GetString("Nombre_paquete"),
                        Descripcion = reader.GetString("Descripcion"),
                        Precio_base = reader.GetDecimal("Precio_base"),
                        Fecha_inicio = reader.IsDBNull("Fecha_inicio") ? null : reader.GetDateTime("Fecha_inicio"),
                        Fecha_fin = reader.IsDBNull("Fecha_fin") ? null : reader.GetDateTime("Fecha_fin"),
                        Capacidad_maxima = reader.IsDBNull("Capacidad_maxima") ? null : reader.GetInt32("Capacidad_maxima"),
                        Estado = reader.GetString("Estado")
                    };
                }
            }

            return paquete;
        }

        // ==================================================
        // LISTAR
        // ==================================================
        public IActionResult Index()
        {
            List<Paquetes> lista = new List<Paquetes>();

            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Paquetes_Turisticos", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Paquetes
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

        // ==================================================
        // CREAR (GET)
        // ==================================================
        public IActionResult Create()
        {
            return View();
        }

        // ==================================================
        // CREAR (POST)
        // ==================================================
        [HttpPost]
        public IActionResult Create(Paquetes paquete)
        {
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();

                var cmd = new MySqlCommand(
                    @"INSERT INTO Paquetes_Turisticos 
                      (Nombre_paquete, Descripcion, Precio_base, Fecha_inicio, Fecha_fin, Capacidad_maxima, Estado)
                      VALUES (@Nombre, @Desc, @Precio, @Inicio, @Fin, @Capacidad, @Estado)", conn);

                cmd.Parameters.AddWithValue("@Nombre", paquete.Nombre_paquete);
                cmd.Parameters.AddWithValue("@Desc", paquete.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", paquete.Precio_base);
                cmd.Parameters.AddWithValue("@Inicio", paquete.Fecha_inicio);
                cmd.Parameters.AddWithValue("@Fin", paquete.Fecha_fin);
                cmd.Parameters.AddWithValue("@Capacidad", paquete.Capacidad_maxima);
                cmd.Parameters.AddWithValue("@Estado", paquete.Estado);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ==================================================
        // DETAILS
        // ==================================================
        public IActionResult Details(int id)
        {
            var paquete = ObtenerPaquete(id);
            if (paquete == null)
                return NotFound();

            return View(paquete);
        }

        // ==================================================
        // EDITAR (GET)
        // ==================================================
        public IActionResult Edit(int id)
        {
            var paquete = ObtenerPaquete(id);
            if (paquete == null)
                return NotFound();

            return View(paquete);
        }

        // ==================================================
        // EDITAR (POST)
        // ==================================================
        [HttpPost]
        public IActionResult Edit(Paquetes paquete)
        {
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();

                var cmd = new MySqlCommand(
                    @"UPDATE Paquetes_Turisticos SET
                        Nombre_paquete = @Nombre,
                        Descripcion = @Desc,
                        Precio_base = @Precio,
                        Fecha_inicio = @Inicio,
                        Fecha_fin = @Fin,
                        Capacidad_maxima = @Capacidad,
                        Estado = @Estado
                      WHERE ID_paquete = @ID", conn);

                cmd.Parameters.AddWithValue("@ID", paquete.ID_paquete);
                cmd.Parameters.AddWithValue("@Nombre", paquete.Nombre_paquete);
                cmd.Parameters.AddWithValue("@Desc", paquete.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", paquete.Precio_base);
                cmd.Parameters.AddWithValue("@Inicio", paquete.Fecha_inicio);
                cmd.Parameters.AddWithValue("@Fin", paquete.Fecha_fin);
                cmd.Parameters.AddWithValue("@Capacidad", paquete.Capacidad_maxima);
                cmd.Parameters.AddWithValue("@Estado", paquete.Estado);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ==================================================
        // ELIMINAR (GET)
        // ==================================================
        public IActionResult Delete(int id)
        {
            var paquete = ObtenerPaquete(id);
            if (paquete == null)
                return NotFound();

            return View(paquete);
        }

        // ==================================================
        // ELIMINAR (POST)
        // ==================================================
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var conn = _conexion.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM Paquetes_Turisticos WHERE ID_paquete = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
