using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ProyectoActividadRepository : BaseRepository<ProyectoActividad>, IProyectoActividadRepository
    {
        public ProyectoActividadRepository(VinculacionContext context)
            : base(context)
        {
        }
        public async Task<bool> ExistsActividadInAnyProyecto(decimal actividadId)
        {
            return await _context.Set<ProyectoActividad>()
                .AnyAsync(x => x.ActividadID == actividadId);
        }

        public async Task<int> CountActividadesByProyecto(decimal proyectoId)
        {
            return await _context.Set<ProyectoActividad>()
                .CountAsync(x => x.ProyectoID == proyectoId);
        }
    }
}