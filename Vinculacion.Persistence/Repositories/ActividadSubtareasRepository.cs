using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActividadSubtareasRepository: BaseRepository<ActividadSubtareas>, IActividadSubtareasRepository
    {
        private readonly VinculacionContext _context;
        public ActividadSubtareasRepository(VinculacionContext context): base(context)
        {
            _context = context;
        }
        public async Task<List<ActividadSubtareas>> GetByActividadIdAsync(decimal actividadId)
        {
            return await _context.ActividadSubtareas
                .Where(s => s.ActividadID == actividadId)
                .OrderBy(s => s.Orden)
                .ToListAsync();
        }
        public async Task<ActividadSubtareas?> GetEntityByIdAsync(decimal id)
        {
            return await _context.ActividadSubtareas
                .FirstOrDefaultAsync(x => x.SubtareaID == id);
        }
    }
}