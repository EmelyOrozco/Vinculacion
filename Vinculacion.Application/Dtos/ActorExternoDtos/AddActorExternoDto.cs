namespace Vinculacion.Application.Dtos.ActorExterno
{
    public class AddActorExternoDto
    {
        public decimal ActorExternoID { get; set; }

        public decimal TipoActorID { get; set; }

        public decimal EstatusID { get; set; } = 1;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? FechaModificacion { get; set; } = null;
    }
}
