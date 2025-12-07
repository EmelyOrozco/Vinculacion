namespace Vinculacion.Application.Dtos.ActorExternoDtos
{
    public class ActorEmpresaResponseDto
    {
        public decimal ActorExternoID { get; set; }
        public decimal EstadoID { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string NombreEmpresa { get; set; }
        public decimal? TipoIdentificacion { get; set; }
        public string IdentificacionNumero { get; set; }
        public decimal? PaisID { get; set; }

        public List<decimal> Clasificaciones { get; set; }
    }
}