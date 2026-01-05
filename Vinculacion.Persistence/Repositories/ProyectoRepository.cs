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

        public async Task<List<ProyectoVinculacion>> GetProyectosEstatusActivo()
        {
            var proyectosActivos = await _context.ProyectoVinculacion.Join(_context.Estado, p => p.EstadoID, e => e.EstadoID, (p, e) => new { Proyecto = p, Estado = e })
             .Where(x =>
                     x.Estado.TablaEstado == "ProyectoVinculacion" &&
                     x.Estado.Descripcion == "Activo")
             .Select(x => x.Proyecto)
             .ToListAsync();

            return proyectosActivos;
        }
    }
}