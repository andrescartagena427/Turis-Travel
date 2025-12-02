using System;

namespace Turis_Travel.Models
{
    public class DashboardReservationItem
    {
        public int IdReserva { get; set; }
        public string Cliente { get; set; }
        public string Paquete { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; }
        public decimal Precio { get; set; }
    }
}
