namespace Presentation.Models
{
    public class ReservacionReport
    {
        public required int ClienteId { get; set; }
        public required int HabitacionId { get; set; }
        public required DateTime FechaEntrada { get; set; }
        public required DateTime FechaSalida { get; set; }
        public required int DiasEstancia { get; set; }
        public required decimal MontoTotal { get; set; }
        public required string Estado { get; set; }
    }
}
