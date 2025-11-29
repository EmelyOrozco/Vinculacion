

namespace Vinculacion.Domain.Entities
{
    public class ActorExterno
    {
        public decimal ActorExternoID { get; set; }

        public decimal TipoActorID { get; set; }

        public decimal EstatusID { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaModificacion { get; set; }

    }
}
