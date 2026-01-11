namespace Vinculacion.Domain.Entities
{
    public class ProyectoVinculacion
    {
        public decimal ProyectoID { get; set; }

        public decimal ActorExternoID { get; set; }
        public decimal PersonaID { get; set; }
        public decimal? RecintoID { get; set; }
        public decimal TipoVinculacionID { get; set; }
        public decimal EstadoID { get; set; }

        public string? TituloProyecto { get; set; }
        public string? DescripcionGeneral { get; set; }

        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public ICollection<ProyectoActividad> ProyectoActividades { get; set; }
            = new List<ProyectoActividad>();
    }
}
