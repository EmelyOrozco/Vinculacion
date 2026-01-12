using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class TipoPersonaVinculacionRepository : ITipoPersonaVinculacionRepository
    {
        private readonly VinculacionContext _context;

        public TipoPersonaVinculacionRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoPersonaVinculacion>> GetAllAsync()
        {
            return await _context.TipoPersonaVinculacion
                .AsNoTracking()
                .OrderBy(x => x.TipoPersonaID)
                .ToListAsync();
        }
    }
}
