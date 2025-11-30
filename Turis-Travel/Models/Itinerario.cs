using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turis_Travel.Models
{
    [Table("Itinerarios")]
    public class Itinerario
    {
        [Key]
        [Column("ID_itinerario")]
        public int ID_itinerario { get; set; }

        [Required]
        [Column("Nombre_itinerario")]
        public string Nombre_itinerario { get; set; }

        [Column("ID_paquete")]
        public int ID_paquete { get; set; }

        [Column("Descripcion")]
        public string Descripcion { get; set; }

        [Column("Categoria")]
        public string Categoria { get; set; }

        [Column("Actividades")]
        public string Actividades { get; set; }

        [Column("Duracion")]
        public int Duracion { get; set; }

        [Column("Fecha_inicio")]
        public DateTime Fecha_inicio { get; set; }

        [Column("Fecha_fin")]
        public DateTime Fecha_fin { get; set; }

        [Column("Precio")]
        public decimal Precio { get; set; }

        [Column("Estado")]
        public string Estado { get; set; }
    }
}

