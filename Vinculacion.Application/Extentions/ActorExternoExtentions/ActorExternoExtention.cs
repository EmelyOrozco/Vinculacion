using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActorExternoExtentions
{
    public static class ActorExternoExtention
    {
        public static ActorExterno ToActorExternoToDto(this AddActorExternoDto actorExterno)
        {
            return new ActorExterno
            {
                ActorExternoID = actorExterno.ActorExternoID,
                TipoActorID = actorExterno.TipoActorID,
                EstadoID = actorExterno.EstadoID,
            };
        }
    }
}
