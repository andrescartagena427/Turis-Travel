using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using Turis_Travel.Models;

namespace Turis_Travel.Controllers
{
    public class ClientesController : Controller
    {
        private readonly string _connString;

        public ClientesController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        // ==========================
        // VISTA -> FORMULARIO
        // ==========================
        public IActionResult Crear()
        {
            return View();
        }

        // ==========================
        // PROCESAR FORMULARIO (POST)
        // ==========================
        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            using var conn = new MySqlConnection(_connString);
            conn.Open();

            string query = @"INSERT INTO clientes 
                            (nombre, cedula, telefono, email, fecha_registro) 
                             VALUES (@nombre, @cedula, @telefono, @email, NOW())";

            using var cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@cedula", cliente.Cedula);
            cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
            cmd.Parameters.AddWithValue("@email", cliente.Email);

            cmd.ExecuteNonQuery();

            TempData["Success"] = "Cliente registrado correctamente.";

            return RedirectToAction("Lista");
        }

        // ==========================
        // LISTADO BÁSICO DE CLIENTES
        // ==========================
        public IActionResult Lista()
        {
            var clientes = new List<Cliente>();

            using var conn = new MySqlConnection(_connString);
            conn.Open();

            string query = @"SELECT id_cliente, nombre, cedula, telefono, email, fecha_registro 
                             FROM clientes ORDER BY fecha_registro DESC";

            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                clientes.Add(new Cliente
                {
                    ID_cliente = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Cedula = reader.GetString(2),
                    Telefono = reader.GetString(3),
                    Email = reader.GetString(4),
                    FechaRegistro = reader.GetDateTime(5)
                });
            }

            return View(clientes);
        }
    }
}
