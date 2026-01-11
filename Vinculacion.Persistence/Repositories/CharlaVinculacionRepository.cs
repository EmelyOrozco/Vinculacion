using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class CharlaVinculacionRepository: BaseRepository<CharlaVinculacion>, ICharlaVinculacionRepository
    {
        public CharlaVinculacionRepository(VinculacionContext context) : base(context)
        {
        }

    }

}
