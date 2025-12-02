
using MediatR;
using Vinculacion.Application.Features.ActorVinculacion.Dtos;

namespace Vinculacion.Application.Features.ActorVinculacion.Commands
{
    public class CreateActorExternoCommand: IRequest<int>
    {
        public required CreateActorExternoDto Actor { get; set; }
    }
}
