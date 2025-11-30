using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turis_Travel.Models
{
    [Table("Transportes")]
    public class Transporte
    {
        [Key]
        [Column("ID_transporte")]
        public int ID_transporte { get; set; }

        [Column("ID_paquete")]
        public int? ID_paquete { get; set; }

        // columna real en tu DB: Tipo_vehiculo
        [Required]
        [Column("Tipo_vehiculo")]
        public string Tipo_vehiculo { get; set; }

        [Column("Capacidad")]
        public int? Capacidad { get; set; }

        [Column("Disponibilidad")]
        public int Disponibilidad { get; set; } = 1;

        [Column("Estado")]
        public string Estado { get; set; } = "activo";
    }
}

