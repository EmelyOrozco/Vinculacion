using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActorExternoService
{
    public interface IActorEmpresaService
    {
        Task<OperationResult<AddActorEmpresaDto>> AddActorEmpresaAsync(AddActorEmpresaDto addActorEmpresaDto, decimal usuarioId);
        Task<OperationResult<List<ActorEmpresaResponseDto>>> GetActorEmpresaAsync();
        Task<OperationResult<ActorEmpresaResponseDto>> GetActorEmpresaById(decimal id);
        Task<OperationResult<bool>> UpdateActorEmpresaAsync(decimal id, UpdateActorEmpresaDto dto, decimal usuarioId);
    }
}