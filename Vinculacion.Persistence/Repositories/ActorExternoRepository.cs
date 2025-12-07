using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActorExternoRepository : BaseRepository<ActorExterno>, IActorExternoRepository
    {
        public ActorExternoRepository(VinculacionContext context) : base(context)
        {
        }
    }
}