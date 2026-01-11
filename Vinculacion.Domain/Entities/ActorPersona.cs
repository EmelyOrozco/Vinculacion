namespace Vinculacion.Domain.Entities
{
    public class ActorPersona
    {
        public decimal ActorExternoID { get; set; }

        public string NombreCompleto { get; set; }

        public decimal? TipoIdentificacion { get; set; }

        public string? IdentificacionNumero { get; set; }

        public string Correo { get; set; }

        public string? Telefono { get; set; }

        public decimal Sexo { get; set; }

        public decimal? PaisID { get; set; }
        public ActorExterno ActorExterno { get; set; }
    }
}
