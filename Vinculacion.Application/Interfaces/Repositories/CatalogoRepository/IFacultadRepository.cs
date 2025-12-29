using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface IFacultadRepository
    {
        Task<IEnumerable<Facultad>> GetAllAsync();
    }
}
