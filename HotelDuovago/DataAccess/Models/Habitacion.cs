namespace DataAccess.Models
{
    public class Habitacion
    {
        public required int Id { get; set; }
        public required int Numero { get; set; }
        public required string Tipo { get; set; }
        public required decimal Precio { get; set; }
        public required int Capacidad { get; set; }
        public required string Descripcion { get; set; }
        public required Boolean Disponible { get; set; }
    }
}
