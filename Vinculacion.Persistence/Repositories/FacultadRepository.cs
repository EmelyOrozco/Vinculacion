using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class FacultadRepository : IFacultadRepository
    {
        private readonly VinculacionContext _context;

        public FacultadRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Facultad>> GetAllAsync()
        {
            return await _context.Facultades
                .AsNoTracking()
                .OrderBy(x => x.Descripcion)
                .ToListAsync();
        }
    }
}
