using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ClasificacionEmpresaRepository : IClasificacionEmpresaRepository
    {
        private readonly VinculacionContext _context;

        public ClasificacionEmpresaRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClasificacionEmpresa>> GetAllAsync()
        {
            return await _context.ClasificacionEmpresa
                .AsNoTracking()
                .OrderBy(x => x.ClasificacionID)
                .ToListAsync();
        }
    }
}
