using Vinculacion.Application.Dtos.DocumentoAdjuntoDto;

namespace Vinculacion.Application.Interfaces.Services.IFileStorageService
{
    public interface IDocumentoAdjuntoService
    {
        Task<IEnumerable<DocumentoAdjuntoDto>> ObtenerArchivosActividadAsync(decimal actividadId);
        Task<IEnumerable<DocumentoAdjuntoDto>> ObtenerArchivosProyectoAsync(decimal proyectoId);
    }
}