
using MediatR;
using Vinculacion.Application.Features.ActorVinculacion.Dtos;

namespace Vinculacion.Application.Features.ActorVinculacion.Commands
{
    public class CreateActorEmpresaCommand: IRequest<int>
    {
        public required AddActorEmpresaDto ActorEmpresa { get; set; }
    }
}
