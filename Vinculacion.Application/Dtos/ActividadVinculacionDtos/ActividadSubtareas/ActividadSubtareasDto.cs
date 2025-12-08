namespace Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas
{
    public class ActividadSubtareasDto
    {
        public decimal ActividadID { get; set; }

        public decimal EstadoID { get; set; }

        public string? TituloSubtarea { get; set; }

        public string? Detalle { get; set; }

        public decimal? Orden { get; set; }

        public DateTime? FechaCompletado { get; set; }
    }
}
