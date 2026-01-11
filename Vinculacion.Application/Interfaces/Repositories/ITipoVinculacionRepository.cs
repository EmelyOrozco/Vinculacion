using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface ITipoVinculacionRepository
    {
        Task<List<TipoVinculacion>> GetAllAsync();
        Task<TipoVinculacion?> GetByIdAsync(decimal id);
    }
}