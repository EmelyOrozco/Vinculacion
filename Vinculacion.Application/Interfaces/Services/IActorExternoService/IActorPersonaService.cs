using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActorExternoService
{
    public interface IActorPersonaService 
    {
        Task<OperationResult<AddActorPersonaDto>> AddActorPersonaAsync(AddActorPersonaDto addActorPersonaDto);
        Task<OperationResult<List<AddActorPersonaDto>>> GetActorPersonaAsync();
        Task<OperationResult<AddActorPersonaDto>> GetActorPersonaById(decimal id);
    }
}