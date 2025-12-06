using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActorEmpresaRepository : BaseRepository<ActorEmpresa>, IActorEmpresaRepository
    {
        public ActorEmpresaRepository(VinculacionContext context) : base(context)
        {
        }


    }
}
