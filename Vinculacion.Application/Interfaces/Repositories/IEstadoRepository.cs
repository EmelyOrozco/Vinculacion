using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface IEstadoRepository
    {
        Task<List<Estado>> GetByTablaAsync(string tablaEstado);
        Task<List<Estado>> GetAllAsync();
    }

}