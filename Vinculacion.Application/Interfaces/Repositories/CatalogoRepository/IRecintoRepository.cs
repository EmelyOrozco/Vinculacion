using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface IRecintoRepository
    {
        Task<IEnumerable<Recinto>> GetAllAsync();
    }
}
