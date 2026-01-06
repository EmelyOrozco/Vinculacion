using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActividadVinculacionService
{
    public interface IActividadVinculacionService
    {
        Task<OperationResult<ActividadVinculacionDto>> AddActividadVinculacion(ActividadVinculacionDto actividadVinculacionDto, decimal usuarioId);
        Task<OperationResult<List<ActividadVinculacionDto>>> GetAllAsync();
        Task<OperationResult<ActividadVinculacionDto>> GetByIdAsync(decimal id);
        Task<OperationResult<bool>> UpdateAsync(decimal id, ActividadVinculacionDto dto, decimal usuarioId);
    }
}
