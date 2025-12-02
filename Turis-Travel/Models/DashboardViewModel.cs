using System;
using System.Collections.Generic;

namespace Turis_Travel.Models
{
    public class DashboardViewModel
    {
        public int ReservasEsteMes { get; set; }
        public decimal IngresosEsteMes { get; set; }
        public int NuevosClientes { get; set; }
        public decimal GananciasTotales { get; set; }
        public int ProximasSalidas { get; set; }

        public List<DashboardReservationItem> UltimasReservas { get; set; } = new List<DashboardReservationItem>();
    }
}
