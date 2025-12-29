using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.DocumentoAdjuntoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class DocumentoAdjuntoRepository : IDocumentoAdjuntoRepository
    {
        private readonly VinculacionContext _context;

        public DocumentoAdjuntoRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DocumentoAdjunto documento)
        {
            await _context.DocumentoAdjunto.AddAsync(documento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentoAdjunto>> GetByProyectoAsync(decimal proyectoId)
        {
            return await _context.DocumentoAdjunto
                .AsNoTracking()
                .Where(x => x.ProyectoID == proyectoId)
                .OrderByDescending(x => x.FechaSubida)
                .ToListAsync();
        }

        public async Task<IEnumerable<DocumentoAdjunto>> GetByActividadAsync(decimal actividadId)
        {
            return await _context.DocumentoAdjunto
                .AsNoTracking()
                .Where(x => x.ActividadID == actividadId)
                .OrderByDescending(x => x.FechaSubida)
                .ToListAsync();
        }
    }
}
