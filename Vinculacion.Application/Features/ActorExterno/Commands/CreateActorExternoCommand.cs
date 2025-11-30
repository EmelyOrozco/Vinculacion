
using MediatR;
using Vinculacion.Application.Features.ActorExterno.Dtos;

namespace Vinculacion.Application.Features.ActorExterno.Commands
{
    public class CreateActorExternoCommand: IRequest<int>
    {
        public required CreateActorExternoDto Actor { get; set; }
    }
}
