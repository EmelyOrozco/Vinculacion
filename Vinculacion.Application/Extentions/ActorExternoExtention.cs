using Vinculacion.Application.Features.ActorVinculacion.Dtos;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions
{
    public static class ActorExternoExtention
    {
        public static ActorExterno ToActorExternoToDto(this AddActorExternoDto actorExterno)
        {
            return new ActorExterno
            {
                ActorExternoID = actorExterno.ActorExternoID,
                TipoActorID = actorExterno.TipoActorID
            };
        }
    }
}
