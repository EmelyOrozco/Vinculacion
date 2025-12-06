using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActorPersonaRepository : BaseRepository<ActorPersona>, IActorPersonaRepository
    {
        public ActorPersonaRepository(VinculacionContext context) : base(context)
        {
        }
    }
}
