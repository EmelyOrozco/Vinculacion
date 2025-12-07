using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository
{
    public interface IActorEmpresaRepository: IBaseRepository<ActorEmpresa>
    {
        Task<List<ActorEmpresa>> GetAllWithClasificacionesAsync();
        Task<ActorEmpresa?> GetByIdWithClasificacionesAsync(decimal id);
    }
}