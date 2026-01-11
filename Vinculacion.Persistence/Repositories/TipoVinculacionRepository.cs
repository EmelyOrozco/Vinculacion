using Microsoft.EntityFrameworkCore;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;
using Vinculacion.Application.Interfaces.Repositories;

namespace Vinculacion.Persistence.Repositories
{
    public class TipoVinculacionRepository : ITipoVinculacionRepository
    {
        private readonly VinculacionContext _context;

        public TipoVinculacionRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<List<TipoVinculacion>> GetAllAsync()
        {
            return await _context.TipoVinculacion.ToListAsync();
        }

        public async Task<TipoVinculacion?> GetByIdAsync(decimal id)
        {
            return await _context.TipoVinculacion
                .FirstOrDefaultAsync(x => x.TipoVinculacionID == id);
        }
    }
}