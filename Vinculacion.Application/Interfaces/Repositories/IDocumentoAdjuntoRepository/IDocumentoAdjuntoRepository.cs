using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.DocumentoAdjuntoRepository
{
    public interface IDocumentoAdjuntoRepository
    {
        Task AddAsync(DocumentoAdjunto documento);
        Task<IEnumerable<DocumentoAdjunto>> GetByProyectoAsync(decimal proyectoId);
        Task<IEnumerable<DocumentoAdjunto>> GetByActividadAsync(decimal actividadId);
    }
}
