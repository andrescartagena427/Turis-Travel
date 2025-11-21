using MySql.Data.MySqlClient;

namespace Turis_Travel.Models
{
    public class UsuarioBD
    {
        private readonly ConexionBD _conexion;

        public UsuarioBD()
        {
            _conexion = new ConexionBD();
        }

        public Usuario ValidarLogin(string correoIngresado, string claveIngresada)
        {
            Usuario u = null;

            using (var conexion = _conexion.GetConnection())
            {
                conexion.Open();

                string query = @"
                    SELECT ID_usuario, Nombre_usuario, Correo, ID_rol, Estado
                    FROM Usuarios
                    WHERE Correo = @correo
                    AND Contrasena = @clave
                    AND Estado = 1";

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@correo", correoIngresado);
                cmd.Parameters.AddWithValue("@clave", claveIngresada);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        u = new Usuario
                        {
                            Id = dr.GetInt32("ID_usuario"),
                            Nombre = dr.GetString("Nombre_usuario"),
                            Correo = dr.GetString("Correo"),
                            IdRol = dr.GetInt32("ID_rol"),
                            Activo = dr.GetBoolean("Estado")
                        };
                    }
                }
            }

            return u;
        }
    }
}
