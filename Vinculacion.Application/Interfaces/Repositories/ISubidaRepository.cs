using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface ISubidaRepository: IBaseRepository<Subida>
    {
        Task<Subida?> GetSubida(decimal subidaId);
    }
}
