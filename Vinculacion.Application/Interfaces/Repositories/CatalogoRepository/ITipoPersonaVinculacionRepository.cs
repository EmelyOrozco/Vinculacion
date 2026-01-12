using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface ITipoPersonaVinculacionRepository
    {
        Task<IEnumerable<TipoPersonaVinculacion>> GetAllAsync();
    }
}