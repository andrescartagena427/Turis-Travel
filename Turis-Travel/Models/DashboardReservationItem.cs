public class DashboardReservationItem
{
    public int IdReserva { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Paquete { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public decimal Precio { get; set; }
    public string Estado { get; set; } = string.Empty;
}
