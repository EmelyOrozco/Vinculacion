namespace Vinculacion.Application.Dtos.ActorExternoDtos
{
    public class ActorEmpresaResponseDto
    {
        public decimal ActorExternoID { get; set; }
        public decimal EstadoID { get; set; }
        public string NombreEmpresa { get; set; }

        public string ContactoNombrePersona { get; set; }

        public string ContactoCorreo { get; set; }

        public string? ContactoTelefono { get; set; }
        public decimal? PaisID { get; set; }

        public List<decimal> Clasificaciones { get; set; }
    }
}