namespace Turis_Travel.Models
{
    public class DashboardViewModel
    {
        // Datos principales
        public int ReservasEsteMes { get; set; }
        public decimal IngresosEsteMes { get; set; }
        public int NuevosClientes { get; set; }
        public int ProximasSalidas { get; set; }

        // Lista última reservas
        public List<DashboardReservationItem> UltimasReservas { get; set; }

        public DashboardViewModel()
        {
            UltimasReservas = new List<DashboardReservationItem>();
        }
    }
}

