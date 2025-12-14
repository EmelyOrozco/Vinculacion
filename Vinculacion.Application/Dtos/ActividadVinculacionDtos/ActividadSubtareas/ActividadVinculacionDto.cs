
namespace Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas
{
    public class ActividadVinculacionDto
    {
        public decimal? ActorExternoId { get; set; }

        public decimal? RecintoId { get; set; }

        public decimal? TipoVinculacionId { get; set; }

        public decimal? PersonaId { get; set; }

        public decimal? EstadoId { get; set; }

        public string? TituloActividad { get; set; }

        public string? DescripcionActividad { get; set; }

        public decimal? Modalidad { get; set; }

        public string? Lugar { get; set; }

        public DateTime? FechaHoraEvento { get; set; }

        public DateTime FechaRegistro { get; set; }

        public decimal? Ambito { get; set; }

        public decimal? Sector { get; set; }

        public List<ActividadSubtareasDto>? Subtareas { get; set; } 
    }
}
