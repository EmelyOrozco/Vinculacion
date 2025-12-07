

using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActividadVinculacionRepository: BaseRepository<ActividadVinculacion>, IActividadVinculacionRepository
    {
        public ActividadVinculacionRepository(VinculacionContext context): base(context)
        {
            
        }
    }
}
