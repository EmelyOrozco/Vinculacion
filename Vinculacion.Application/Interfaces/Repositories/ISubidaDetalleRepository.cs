using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface ISubidaDetalleRepository: IBaseRepository<SubidaDetalle>
    {
        Task<IEnumerable<SubidaDetalle>> GetBySubidaIdAsync(decimal subidaId);
    }
}
