
namespace Vinculacion.Domain.Entities
{
    public class Notificacion
    {
        public decimal NotificacionID { get; set; }

        public decimal UsuarioID { get; set; }

        public string Titulo { get; set; } = null!;

        public string Mensaje { get; set; } = null!;

        public string Tipo { get; set; } = null!;
        // Proyecto | Actividad | Sistema

        public decimal? ReferenciaID { get; set; }

        public bool Leida { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
