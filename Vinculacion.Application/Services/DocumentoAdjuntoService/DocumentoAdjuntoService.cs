using Vinculacion.Application.Dtos.DocumentoAdjuntoDto;
using Vinculacion.Application.Interfaces.Repositories.DocumentoAdjuntoRepository;
using Vinculacion.Application.Interfaces.Services.IFileStorageService;

namespace Vinculacion.Application.Services.DocumentoAdjuntoService
{
    public class DocumentoAdjuntoService : IDocumentoAdjuntoService
    {
        private readonly IDocumentoAdjuntoRepository _documentoAdjuntoRepository;

        public DocumentoAdjuntoService(IDocumentoAdjuntoRepository documentoAdjuntoRepository)
        {
            _documentoAdjuntoRepository = documentoAdjuntoRepository;
        }

        public async Task<IEnumerable<DocumentoAdjuntoDto>> ObtenerArchivosActividadAsync(decimal actividadId)
        {
            var archivos = await _documentoAdjuntoRepository.GetByActividadAsync(actividadId);

            return archivos.Select(x => new DocumentoAdjuntoDto
            {
                DocumentoAdjuntoID = x.DocumentoAdjuntoID,
                NombreOriginal = x.NombreOriginal,
                Ruta = x.Ruta,
                TipoMime = x.TipoMime,
                FechaSubida = x.FechaSubida
            });
        }
        public async Task<IEnumerable<DocumentoAdjuntoDto>> ObtenerArchivosProyectoAsync(decimal proyectoId)
        {
            var archivos = await _documentoAdjuntoRepository.GetByProyectoAsync(proyectoId);

            return archivos.Select(x => new DocumentoAdjuntoDto
            {
                DocumentoAdjuntoID = x.DocumentoAdjuntoID,
                NombreOriginal = x.NombreOriginal,
                Ruta = x.Ruta,
                TipoMime = x.TipoMime,
                FechaSubida = x.FechaSubida
            });
        }
    }
}
