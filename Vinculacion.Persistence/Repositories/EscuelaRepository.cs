using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class EscuelaRepository : IEscuelaRepository
    {
        private readonly VinculacionContext _context;

        public EscuelaRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Escuela>> GetByFacultadAsync(decimal facultadId)
        {
            return await _context.Escuelas
                .AsNoTracking()
                .Where(x => x.FacultadID == facultadId)
                .OrderBy(x => x.Descripcion)
                .ToListAsync();
        }
    }
}
