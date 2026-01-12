using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface IFacultadRepository
    {
        Task<Facultad?> GetByIdAsync(decimal facultadId);
        Task<IEnumerable<Facultad>> GetAllAsync();
    }
}
