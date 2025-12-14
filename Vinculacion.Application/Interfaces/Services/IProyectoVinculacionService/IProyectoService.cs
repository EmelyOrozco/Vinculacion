using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Services.IProyectoVinculacionService
{
    public interface IProyectoService
    {
        Task<OperationResult<AddProyectoDto>> AddProyectoAsync(AddProyectoDto request);
        Task<OperationResult<List<ProyectoResponseDto>>> GetProyectosAsync();
        Task<OperationResult<ProyectoResponseDto>> GetProyectoByIdAsync(decimal proyectoId);
        Task<OperationResult<bool>> UpdateProyectoAsync(decimal proyectoId, UpdateProyectoDto dto);
        Task<OperationResult<bool>> AddActividadToProyectoAsync(decimal proyectoId, decimal actividadId);
        Task<OperationResult<bool>> AddActividadesToProyectoAsync(decimal proyectoId,AddActividadesToProyectoDto dto);
        Task<OperationResult<List<ActividadVinculacionDto>>>GetActividadesByProyectoAsync(decimal proyectoId);
        Task<OperationResult<List<ActividadVinculacionDto>>> GetActividadesDisponiblesAsync();
    }
}