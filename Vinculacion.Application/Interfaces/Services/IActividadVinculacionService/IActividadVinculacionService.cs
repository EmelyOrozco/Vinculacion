using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActividadVinculacionService
{
    public interface IActividadVinculacionService
    {
        Task<OperationResult<ActividadVinculacionDto>> AddActividadVinculacion(ActividadVinculacionDto actividadVinculacionDto, decimal usuarioId);
        Task<OperationResult<List<ResponseActividadVinculacionDto>>> GetAllAsync();
        Task<OperationResult<ResponseActividadVinculacionDto>> GetByIdAsync(decimal id);
        Task<OperationResult<bool>> UpdateAsync(decimal id, ActividadVinculacionDto dto, decimal usuarioId);
        Task ProcesarActividadesAsync(DateTime hoy);
        Task EnviarAlertasActividadesAsync(DateTime hoy);
    }
}
