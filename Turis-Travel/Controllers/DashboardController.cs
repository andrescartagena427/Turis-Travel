using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
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
            var rol = HttpContext.Session.GetInt32("IdRol");

            if (rol == null)
                return RedirectToAction("Index", "Login");

            if (rol != 1)
                return RedirectToAction("Index", "Home");

            var model = new DashboardViewModel();

            using (var conn = new MySqlConnection(_connString))
            {
                conn.Open();

                // 1️⃣ Reservas este mes
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

                // 3️⃣ Clientes nuevos este mes
                using (var cmd = new MySqlCommand(@"
            SELECT COUNT(*)
            FROM Clientes
            WHERE YEAR(Fecha_registro) = @y 
              AND MONTH(Fecha_registro) = @m", conn))
                {
                    cmd.Parameters.AddWithValue("@y", DateTime.Now.Year);
                    cmd.Parameters.AddWithValue("@m", DateTime.Now.Month);
                    model.NuevosClientes = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 4️⃣ Ganancias totales
                using (var cmd = new MySqlCommand(@"
            SELECT IFNULL(SUM(Precio_total), 0)
            FROM Reservas", conn))
                {
                    model.GananciasTotales = Convert.ToDecimal(cmd.ExecuteScalar());
                }

                // 5️⃣ Próximas salidas
                using (var cmd = new MySqlCommand(@"
            SELECT COUNT(*)
            FROM Paquetes_Turisticos
            WHERE Fecha_inicio >= @hoy
              AND Estado = 'activo'", conn))
                {
                    cmd.Parameters.AddWithValue("@hoy", DateTime.Today);
                    model.ProximasSalidas = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 6️⃣ Últimas reservas (ya corregido)
                using (var cmd = new MySqlCommand(@"
            SELECT 
                r.ID_reserva,
                c.Nombre AS ClienteNombre,
                p.Nombre_paquete,
                r.Fecha_solicitud,
                r.Estado,
                r.Precio_total
            FROM Reservas r
            LEFT JOIN Clientes c ON r.ID_cliente = c.ID_cliente
            LEFT JOIN Paquetes_Turisticos p ON r.ID_paquete = p.ID_paquete
            ORDER BY r.Fecha_solicitud DESC
            LIMIT 6", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new DashboardReservationItem();

                            item.IdReserva = reader["ID_reserva"] != DBNull.Value
                                ? Convert.ToInt32(reader["ID_reserva"])
                                : 0;

                            item.Cliente = reader["ClienteNombre"] == DBNull.Value
                                ? "Cliente eliminado"
                                : reader["ClienteNombre"].ToString();

                            item.Paquete = reader["Nombre_paquete"] == DBNull.Value
                                ? "—"
                                : reader["Nombre_paquete"].ToString();

                            item.FechaInicio = reader["Fecha_solicitud"] != DBNull.Value
                                ? Convert.ToDateTime(reader["Fecha_solicitud"])
                                : DateTime.Now;

                            item.Estado = reader["Estado"] != DBNull.Value
                                ? reader["Estado"].ToString()
                                : "—";

                            item.Precio = reader["Precio_total"] != DBNull.Value
                                ? Convert.ToDecimal(reader["Precio_total"])
                                : 0;

                            model.UltimasReservas.Add(item);
                        }
                    }
                }
            }

            return View(model);
        }

    }
}


