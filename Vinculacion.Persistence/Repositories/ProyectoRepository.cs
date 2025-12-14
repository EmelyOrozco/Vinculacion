using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ProyectoRepository : BaseRepository<ProyectoVinculacion>, IProyectoRepository
    {
        public ProyectoRepository(VinculacionContext context)
            : base(context)
        {
        }

        public async Task<List<ProyectoVinculacion>> GetAllWithActividadesAsync()
        {
            return await _context.Set<ProyectoVinculacion>()
                .Include(p => p.ProyectoActividades)
                .ToListAsync();
        }

        public async Task<ProyectoVinculacion?> GetByIdWithActividadesAsync(decimal proyectoId)
        {
            return await _context.Set<ProyectoVinculacion>()
                .Include(p => p.ProyectoActividades)
                .FirstOrDefaultAsync(p => p.ProyectoID == proyectoId);
        }
    }
}