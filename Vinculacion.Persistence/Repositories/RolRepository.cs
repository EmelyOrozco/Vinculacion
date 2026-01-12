using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly VinculacionContext _context;

        public RolRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Rol
                .AsNoTracking()
                .OrderBy(x => x.Idrol)
                .ToListAsync();
        }
    }
}
