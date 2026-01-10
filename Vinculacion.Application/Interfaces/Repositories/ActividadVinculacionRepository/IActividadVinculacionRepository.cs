using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository
{
    public interface IActividadVinculacionRepository: IBaseRepository<ActividadVinculacion>
    {
        Task<List<ActividadVinculacion>> GetActividadesByProyectoId(decimal proyectoId);
        Task<List<ActividadVinculacion>> GetActividadesDisponibles();
        Task<OperationResult<List<ActividadVinculacion>>> GetAllWithSubtareasAsync();
        Task<OperationResult<ActividadVinculacion>> GetByIdWithSubtareasAsync(decimal id);
        Task<List<ActividadVinculacion>>GetActividadesDisponiblesByActorExterno(decimal actorExternoId);
        Task<List<ActividadVinculacion>> GetActividadEstatusActivo();
    }
}
