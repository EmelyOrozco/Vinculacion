using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository
{
    public interface IActividadVinculacionRepository: IBaseRepository<ActividadVinculacion>
    {
        Task<List<ActividadVinculacion>> GetActividadesByProyectoId(decimal proyectoId);
        Task<List<ActividadVinculacion>> GetActividadesDisponibles();
    }
}
