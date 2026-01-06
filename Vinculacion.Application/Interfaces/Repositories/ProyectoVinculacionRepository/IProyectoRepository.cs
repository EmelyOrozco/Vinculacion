using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository
{
    public interface IProyectoRepository : IBaseRepository<ProyectoVinculacion>
    {
        Task<List<ProyectoVinculacion>> GetAllWithActividadesAsync();
        Task<ProyectoVinculacion?> GetByIdWithActividadesAsync(decimal proyectoId);
        Task<List<ProyectoVinculacion>> GetProyectosEstatusActivo();
    }
}