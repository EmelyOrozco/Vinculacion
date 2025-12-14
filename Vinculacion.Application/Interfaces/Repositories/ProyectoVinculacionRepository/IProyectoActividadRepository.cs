using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository
{
    public interface IProyectoActividadRepository : IBaseRepository<ProyectoActividad>
    {
        Task<bool> ExistsActividadInAnyProyecto(decimal actividadId);
        Task<int> CountActividadesByProyecto(decimal proyectoId);
    }
}