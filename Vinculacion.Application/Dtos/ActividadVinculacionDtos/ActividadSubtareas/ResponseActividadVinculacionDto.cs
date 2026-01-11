namespace Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas
{
    public class ResponseActividadVinculacionDto
    {
        public decimal ActorExternoId { get; set; }

        public decimal? RecintoId { get; set; }

        public decimal TipoVinculacionId { get; set; }

        public decimal EstadoID { get; set; }
        public decimal PersonaId { get; set; }

        public string? TituloActividad { get; set; }

        public string? DescripcionActividad { get; set; }

        public decimal? Modalidad { get; set; }

        public string? Lugar { get; set; }

        public DateTime? FechaHoraEvento { get; set; }

        public DateTime FechaRegistro { get; set; }

        public List<ActividadSubtareaDto>? Subtareas { get; set; }
    }
}
