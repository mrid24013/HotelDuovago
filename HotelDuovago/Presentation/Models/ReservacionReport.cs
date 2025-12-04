namespace Presentation.Models
{
    public class ReservacionReport
    {
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
        public required string Email { get; set; }
        public required string Direccion { get; set; }
        public required DateTime FechaRegistro { get; set; }
    }
}
