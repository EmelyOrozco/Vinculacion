using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface ICarreraRepository
    {
        Task<IEnumerable<Carrera>> GetByEscuelaAsync(decimal escuelaId);
    }
}
