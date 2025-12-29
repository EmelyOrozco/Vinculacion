using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class RecintoRepository : IRecintoRepository
    {
        private readonly VinculacionContext _context;

        public RecintoRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recinto>> GetAllAsync()
        {
            return await _context.Recintos
                .AsNoTracking()
                .OrderBy(x => x.Descripcion)
                .ToListAsync();
        }
    }
}
