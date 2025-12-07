namespace Vinculacion.Domain.Entities
{
    public class ActorEmpresa
    {
        public decimal ActorExternoID { get; set; }

        public string NombreEmpresa { get; set; }

        public decimal? TipoIdentificacion { get; set; }

        public string IdentificacionNumero { get; set; }

        public string ContactoNombrePersona { get; set; }

        public string ContactoCorreo { get; set; }

        public string ContactoTelefono { get; set; }

        public decimal? ContactoSexoPersona { get; set; }

        public decimal? PaisID { get; set; }
        public ActorExterno ActorExterno { get; set; }
        public ICollection<ActorEmpresaClasificacion> ActorEmpresaClasificaciones { get; set; }
            = new List<ActorEmpresaClasificacion>();
    }
}