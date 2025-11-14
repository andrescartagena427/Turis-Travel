using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Turis_Travel.Models;


namespace Turis_Travel.Models
{
    [Table("Paquetes_Turisticos")] 
    public class Paquetes
    {
        [Key]
        public int ID_paquete { get; set; }

        [Required]
        public string Nombre_paquete { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public decimal Precio_base { get; set; }

        public DateTime? Fecha_inicio { get; set; }
        public DateTime? Fecha_fin { get; set; }

        public int? Capacidad_maxima { get; set; }

        public string Estado { get; set; }
    }
}
