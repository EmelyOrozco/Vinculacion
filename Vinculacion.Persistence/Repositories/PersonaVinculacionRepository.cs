using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class PersonaVinculacionRepository: BaseRepository<PersonaVinculacion>, IPersonaVinculacionRepository
    {
        public PersonaVinculacionRepository(VinculacionContext context) : base(context)
        {
            
        }
    }
}
