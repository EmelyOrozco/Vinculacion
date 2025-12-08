using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActividadSubtareasRepository: BaseRepository<ActividadSubtareas>, IActividadSubtareasRepository
    {
        public ActividadSubtareasRepository(VinculacionContext context): base(context)
        {
            
        }
    }
}
