using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository
{
    public interface IActividadSubtareasRepository: IBaseRepository<ActividadSubtareas>
    {
        Task<ActividadSubtareas?> GetEntityByIdAsync(decimal id);
        Task<List<ActividadSubtareas>> GetByActividadIdAsync(decimal actividadId);
    }
}
