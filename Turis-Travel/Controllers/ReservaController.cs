using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class ReservaController : Controller
    {
        private readonly string _connString;

        public ReservaController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        // ======================================================
        // LISTA
        // ======================================================
        public IActionResult Lista()
        {
            var lista = new List<Reserva>();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string query = @"SELECT ID_reserva, ID_cliente, ID_paquete,
                                 ID_itinerario, ID_transporte, Fecha_solicitud,
                                 Estado, Numero_pasajeros, Precio_total
                                 FROM Reservas
                                 ORDER BY Fecha_solicitud DESC";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Reserva
                        {
                            ID_reserva = reader.GetInt32(0),
                            ID_cliente = reader.GetInt32(1),
                            ID_paquete = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            ID_itinerario = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            ID_transporte = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4),
                            Fecha_solicitud = reader.GetDateTime(5),
                            Estado = reader.GetString(6),
                            Numero_pasajeros = reader.GetInt32(7),
                            Precio_total = reader.GetDecimal(8)
                        });
                    }
                }
            }

            return View(lista);
        }

        // ======================================================
        // CREAR GET
        // ======================================================
        public IActionResult Crear()
        {
            CargarCombos();
            return View();
        }

        // ======================================================
        // CREAR POST
        // ======================================================
        [HttpPost]
        public IActionResult Crear(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                CargarCombos();
                return View(reserva);
            }

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();

                string query = @"INSERT INTO Reservas 
                                (ID_cliente, ID_paquete, ID_itinerario, ID_transporte,
                                 Estado, Numero_pasajeros, Precio_total, Fecha_solicitud)
                                 VALUES (@cliente, @paq, @iti, @tra, @estado,
                                         @num, @precio, @fecha)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cliente", reserva.ID_cliente);
                    cmd.Parameters.Add("@paq", MySqlDbType.Int32).Value = reserva.ID_paquete ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@iti", MySqlDbType.Int32).Value = reserva.ID_itinerario ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@tra", MySqlDbType.Int32).Value = reserva.ID_transporte ?? (object)DBNull.Value;
                    cmd.Parameters.AddWithValue("@estado", reserva.Estado);
                    cmd.Parameters.AddWithValue("@num", reserva.Numero_pasajeros);
                    cmd.Parameters.AddWithValue("@precio", reserva.Precio_total);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "Reserva creada correctamente ✔";
            return RedirectToAction("Lista");
        }

        // ======================================================
        // MÉTODOS AUXILIARES
        // ======================================================
        private void CargarCombos()
        {
            ViewBag.Clientes = ObtenerClientes();
            ViewBag.Paquetes = ObtenerPaquetes();
            ViewBag.Itinerarios = ObtenerItinerarios();
            ViewBag.Transportes = ObtenerTransportes();
        }

        private List<Cliente> ObtenerClientes()
        {
            var lista = new List<Cliente>();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string query = "SELECT ID_cliente, Nombre FROM Clientes ORDER BY Nombre ASC";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Cliente
                        {
                            ID_cliente = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }

            return lista;
        }

        private List<Paquete> ObtenerPaquetes()
        {
            var lista = new List<Paquete>();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string query = "SELECT ID_paquete, Nombre_paquete FROM Paquetes_Turisticos ORDER BY Nombre_paquete";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Paquete
                        {
                            ID_paquete = reader.GetInt32(0),
                            Nombre_paquete = reader.GetString(1)
                        });
                    }
                }
            }

            return lista;
        }

        private List<Itinerario> ObtenerItinerarios()
        {
            var lista = new List<Itinerario>();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string query = "SELECT ID_itinerario, Nombre_itinerario FROM Itinerarios ORDER BY Nombre_itinerario";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Itinerario
                        {
                            ID_itinerario = reader.GetInt32(0),
                            Nombre_itinerario = reader.GetString(1)
                        });
                    }
                }
            }

            return lista;
        }

        private List<Transporte> ObtenerTransportes()
        {
            var lista = new List<Transporte>();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();
                string query = "SELECT ID_transporte, Tipo_vehiculo FROM Transportes ORDER BY Tipo_vehiculo";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Transporte
                        {
                            ID_transporte = reader.GetInt32(0),
                            Tipo_vehiculo = reader.GetString(1)
                        });
                    }
                }
            }
            return lista;
        }
    }
}

