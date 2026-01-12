using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface ICarreraRepository
    {
        Task<Carrera?> GetByIdAsync(decimal carreraId);
        Task<IEnumerable<Carrera>> GetByEscuelaAsync(decimal escuelaId);
    }
}
