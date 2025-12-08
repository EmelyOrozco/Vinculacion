using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActorEmpresaClasificacionRepository: BaseRepository<ActorEmpresaClasificacion>, IActorEmpresaClasificacionRepository
    {
        public ActorEmpresaClasificacionRepository(VinculacionContext context): base(context)
        {
        }
    }
}
