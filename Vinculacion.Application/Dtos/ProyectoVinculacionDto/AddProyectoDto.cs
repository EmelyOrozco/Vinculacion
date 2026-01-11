namespace Vinculacion.Application.Dtos.ProyectoVinculacionDto
{
    public class AddProyectoDto
    {
        public decimal ActorExternoID { get; set; }
        public decimal PersonaID { get; set; }
        public decimal? RecintoID { get; set; }
        public decimal TipoVinculacionID { get; set; }
        public string? TituloProyecto { get; set; }
        public string? DescripcionGeneral { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public decimal? Ambito { get; set; }
        public decimal? Sector { get; set; }
    }
}
