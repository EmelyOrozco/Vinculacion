namespace Vinculacion.Application.Dtos.ActorExternoDtos
{
    public class AddActorEmpresaDto
    {
        public string NombreEmpresa { get; set; }

        public decimal? TipoIdentificacion { get; set; }

        public string IdentificacionNumero { get; set; }

        public string ContactoNombrePersona { get; set; }

        public string ContactoCorreo { get; set; }

        public string ContactoTelefono { get; set; }

        public decimal? ContactoSexoPersona { get; set; }

        public decimal? PaisID { get; set; }

        public List<decimal> Clasificaciones { get; set; }

    }
}
