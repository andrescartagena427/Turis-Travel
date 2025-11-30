using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turis_Travel.Models
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        public int ID_reserva { get; set; }

        [Required]
        public int ID_cliente { get; set; }

        public int? ID_paquete { get; set; }
        public int? ID_itinerario { get; set; }
        public int? ID_transporte { get; set; }

        public DateTime Fecha_solicitud { get; set; } = DateTime.Now;

        public string Estado { get; set; } = "pendiente";

        public int Numero_pasajeros { get; set; }
        public decimal Precio_total { get; set; }
    }
}

