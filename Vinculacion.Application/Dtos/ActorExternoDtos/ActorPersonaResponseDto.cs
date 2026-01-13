namespace Vinculacion.Application.Dtos.ActorExternoDtos
{
    public class ActorPersonaResponseDto
    {
        public decimal ActorExternoID { get; set; }
        public decimal EstadoID { get; set; }
        public string NombreCompleto { get; set; }
        public decimal? TipoIdentificacion { get; set; }

        public string? IdentificacionNumero { get; set; }
        public string Correo { get; set; }

        public string? Telefono { get; set; }
        public decimal? PaisID { get; set; }
        public decimal Sexo { get; set; }
    }
}
