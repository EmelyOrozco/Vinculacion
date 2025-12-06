using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.ActorExterno
{
    public interface IActorPersonaService 
    {
        Task<OperationResult<AddActorPersonaDto>> AddActorPersonaAsync(AddActorPersonaDto addActorPersonaDto, AddActorExternoDto addActorExternoDto);
    }
}
