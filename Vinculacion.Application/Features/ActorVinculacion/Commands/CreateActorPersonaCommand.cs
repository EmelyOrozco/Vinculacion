using MediatR;
using Vinculacion.Application.Features.ActorVinculacion.Dtos;

namespace Vinculacion.Application.Features.ActorVinculacion.Commands
{
    public class CreateActorPersonaCommand: IRequest<int>
    {
        public required CreateActorPersonaDto ActorPersona { get; set; }
    }
}
