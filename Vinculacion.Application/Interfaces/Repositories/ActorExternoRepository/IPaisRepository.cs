using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository
{
    public interface IPaisRepository: IBaseRepository<Pais>
    {
        Task<bool> PaisExists(decimal? PaisID);
    }
}
