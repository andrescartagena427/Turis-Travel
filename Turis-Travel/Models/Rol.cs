using System.ComponentModel.DataAnnotations;

namespace Turis_Travel.Models
{
    public class Rol
    {
        [Key]
        public int ID_rol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(80)]
        public string Nombre_rol { get; set; } = string.Empty;

        public int Estado_rol { get; set; }
    }
}
