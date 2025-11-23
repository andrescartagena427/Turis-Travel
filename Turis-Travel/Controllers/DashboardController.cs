using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class DashboardController : Controller
    {
        private readonly string _connString = string.Empty;

        public DashboardController(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection")!;
        }

        public IActionResult Index()
        {
            // Validación de sesión
            var rol = HttpContext.Session.GetInt32("IdRol");
            if (rol == null) return RedirectToAction("Index", "Login");
            if (rol != 1) return RedirectToAction("Index", "Home");

            var model = new DashboardViewModel();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();

                // 1️⃣ Total de reservas este mes
                using (var cmd = new MySqlCommand(@"
                    SELECT COUNT(*) 
                    FROM Reservas
                    WHERE YEAR(Fecha_solicitud) = @y 
                      AND MONTH(Fecha_solicitud) = @m", conn))
                {
                    cmd.Parameters.AddWithValue("@y", DateTime.Now.Year);
                    cmd.Parameters.AddWithValue("@m", DateTime.Now.Month);
                    model.ReservasEsteMes = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2️⃣ Ingresos este mes
                using (var cmd = new MySqlCommand(@"
                    SELECT IFNULL(SUM(Precio_total), 0)
                    FROM Reservas
                    WHERE YEAR(Fecha_solicitud) = @y 
                      AND MONTH(Fecha_solicitud) = @m", conn))
                {
                    cmd.Parameters.AddWithValue("@y", DateTime.Now.Year);
                    cmd.Parameters.AddWithValue("@m", DateTime.Now.Month);
                    model.IngresosEsteMes = Convert.ToDecimal(cmd.ExecuteScalar());
                }

                // 3️⃣ Nuevos clientes 
                // (tu tabla NO tiene Fecha_creacion → contamos usuarios donde Estado = 1)
                using (var cmd = new MySqlCommand(@"
                    SELECT COUNT(*)
                    FROM Usuarios
                    WHERE Estado = 1", conn))
                {
                    model.NuevosClientes = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 4️⃣ Próximas salidas
                using (var cmd = new MySqlCommand(@"
                    SELECT COUNT(*)
                    FROM Paquetes_Turisticos
                    WHERE Fecha_inicio >= @hoy
                      AND Estado = 'activo'", conn))
                {
                    cmd.Parameters.AddWithValue("@hoy", DateTime.Today);
                    model.ProximasSalidas = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 5️⃣ Últimas reservas
                using (var cmd = new MySqlCommand(@"
                    SELECT 
                        r.ID_reserva,
                        u.Nombre_usuario,
                        p.Nombre_paquete,
                        r.Fecha_solicitud,
                        r.Estado,
                        r.Precio_total
                    FROM Reservas r
                    LEFT JOIN Usuarios u ON r.ID_usuario = u.ID_usuario
                    LEFT JOIN Paquetes_Turisticos p ON r.ID_paquete = p.ID_paquete
                    ORDER BY r.Fecha_solicitud DESC
                    LIMIT 6", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.UltimasReservas.Add(new DashboardReservationItem
                            {
                                IdReserva = reader.GetInt32("ID_reserva"),
                                Cliente = reader.IsDBNull("Nombre_usuario") ? "—" : reader.GetString("Nombre_usuario"),
                                Paquete = reader.IsDBNull("Nombre_paquete") ? "—" : reader.GetString("Nombre_paquete"),
                                FechaInicio = reader.GetDateTime("Fecha_solicitud"),
                                Estado = reader.GetString("Estado"),
                                Precio = reader.GetDecimal("Precio_total")
                            });
                        }
                    }
                }
            }

            return View(model);
        }
    }
}
