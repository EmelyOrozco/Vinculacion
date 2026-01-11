using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActividadVinculacionService
{
    public interface IActividadSubtareasService
    {
        Task<OperationResult<List<ActividadSubtareaDto>>> GetByActividadAsync(decimal actividadId);
        Task<OperationResult<ActividadSubtareaDto>> CreateAsync(decimal actividadId, ActividadSubtareaCreateDto dto);
        Task<OperationResult<bool>> UpdateAsync(decimal subtareaId, ActividadSubtareaUpdateDto dto);
    }
}