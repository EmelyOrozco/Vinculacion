

namespace Vinculacion.Application.Features.ActorVinculacion.Dtos
{
    public record class CreateActorPersonaDto
    {

        public string NombreCompleto { get; set; }

        public decimal? TipoIdentificacion { get; set; }

        public string IdentificacionNumero { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public decimal? Sexo { get; set; }

        public decimal? PaisID { get; set; }
    }
}
