using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IProyectoVinculacionService
{
    public interface IProyectoService
    {
        Task<OperationResult<AddProyectoDto>> AddProyectoAsync(AddProyectoDto request, decimal usuarioId);
        Task<OperationResult<List<ProyectoResponseDto>>> GetProyectosAsync();
        Task<OperationResult<ProyectoResponseDto>> GetProyectoByIdAsync(decimal proyectoId);
        Task<OperationResult<bool>> UpdateProyectoAsync(decimal proyectoId, UpdateProyectoDto dto, decimal usuarioId);
        Task<OperationResult<bool>> AddActividadToProyectoAsync(decimal proyectoId, decimal actividadId, decimal usuarioId);
        Task<OperationResult<bool>> AddActividadesToProyectoAsync(decimal proyectoId,AddActividadesToProyectoDto dto, decimal usuarioId);
        Task<OperationResult<List<ActividadVinculacionDto>>>GetActividadesByProyectoAsync(decimal proyectoId);
        Task<OperationResult<List<ActividadVinculacionDto>>> GetActividadesDisponiblesAsync();
        Task<OperationResult<List<ActividadVinculacionDto>>>GetActividadesDisponiblesByProyectoAsync(decimal proyectoId);

        Task ProcesarProyectosAsync(DateTime hoy);
        Task EnviarAlertasProyectosAsync(DateTime hoy);
    }
}