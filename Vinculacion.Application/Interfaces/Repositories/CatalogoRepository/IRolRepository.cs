using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllAsync();
    }
}