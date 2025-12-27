using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface IEscuelaRepository
    {
        Task<IEnumerable<Escuela>> GetByFacultadAsync(decimal facultadId);
    }
}
