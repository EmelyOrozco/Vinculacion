namespace Vinculacion.Application.Features.ActorExterno.Dtos
{
    public class CreateActorExternoDto
    {
        public decimal TipoActorID { get; set; }
        public decimal EstatusID { get; set; }

        public string NombreCompleto { get; set; }
        public decimal? TipoIdentificacion { get; set; }
        public string IdentificacionNumero { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public decimal? Sexo { get; set; }
        public decimal? PaisID { get; set; }


        public string NombreEmpresa { get; set; }
        public string ContactoNombrePersona { get; set; }
        public string ContactoCorreo { get; set; }
        public string ContactoTelefono { get; set; }
        public decimal? ContactoSexoPersona { get; set; }
    }
}
