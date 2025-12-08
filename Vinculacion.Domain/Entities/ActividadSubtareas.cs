
namespace Vinculacion.Domain.Entities
{
    public class ActividadSubtareas
    {
        public decimal SubtareaID { get; set; }

        public decimal ActividadID { get; set; }

        public decimal? EstadoID { get; set; }

        public string? TituloSubtarea { get; set; }

        public string? Detalle { get; set; }

        public decimal? Orden { get; set; }

        public DateTime? FechaCompletado { get; set; }

        public ActividadVinculacion ActividadVinculacion { get; set; }
    }
}
