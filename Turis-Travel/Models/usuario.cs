namespace Turis_Travel.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }   // <-- ID_rol de tu BD
        public string Rol {get; set; }    
        public bool Activo { get; set; }
    }
}
