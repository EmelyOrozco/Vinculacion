using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActorExternoService
{
    public interface IActorPersonaService 
    {
        Task<OperationResult<AddActorPersonaDto>> AddActorPersonaAsync(AddActorPersonaDto addActorPersonaDto, decimal usuarioId);
        Task<OperationResult<List<AddActorPersonaDto>>> GetActorPersonaAsync();
        Task<OperationResult<AddActorPersonaDto>> GetActorPersonaById(decimal id);
        Task<OperationResult<bool>> UpdateActorPersonaAsync(decimal id,UpdateActorPersonaDto dto, decimal usuarioId);
    }
}