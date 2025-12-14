using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository
{
    public interface IActorPersonaRepository: IBaseRepository<ActorPersona>
    {
        Task<ActorPersona?> GetByIdWithActorExternoAsync(decimal id);
        Task<bool> ActorPersonaExists(string? Identificacion);
    }
}
