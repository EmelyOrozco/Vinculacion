using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class PasantiaVinculacionRepository: BaseRepository<PasantiaVinculacion>, IPasantiaVinculacionRepository
    {
        public PasantiaVinculacionRepository(VinculacionContext context): base(context)
        {
            
        }
    }
}
