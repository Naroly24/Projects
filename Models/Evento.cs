using System.ComponentModel.DataAnnotations;

namespace AppCRUD.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public required string Ubicacion { get; set; }
        [Range(1, 10000, ErrorMessage = "Capacidad debe estar entre 1 y 10000")]
        public int CapacidadMaxima { get; set; }
    }
}