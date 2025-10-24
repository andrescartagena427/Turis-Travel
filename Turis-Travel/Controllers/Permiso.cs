using System.ComponentModel.DataAnnotations;
namespace Turis_Travel.Models
{
    public class Permiso
    {
        [Key]
        public int ID_permiso { get; set; }

        [Required(ErrorMessage = "El ID del rol no es obligatorio")]
        public int ID_rol { get; set; }

        [Required(ErrorMessage = "El ID del módulo es obligatorio")]
        public int ID_modulo { get; set; }

        public int Estado_permiso { get; set; }

        // Campos para mostrar nombres en las vistas
        public string? Nombre_rol { get; set; }
        public string? Nombre_modulo { get; set; }
    }
}
