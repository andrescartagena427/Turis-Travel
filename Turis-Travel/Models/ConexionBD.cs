using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

namespace Turis_Travel.Models
{
    public class ConexionBD
    {
        private readonly string connectionString;

        public ConexionBD()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = builder.GetConnectionString("DefaultConnection") ?? "";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
