using Microsoft.AspNetCore.Http;
using Vinculacion.Application.Interfaces.Repositories.DocumentoAdjuntoRepository;
using Vinculacion.Application.Interfaces.Services.IFileStorageService;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.SubirArchivoExtentions
{
    public static class SubirArchivoActividadExtensions
    { 
        public static async Task SubirArchivoActividadExtention(decimal actividadId, IFormFile file, 
                                                                decimal usuarioId, IFileStorageService fileStorageService, 
                                                                IDocumentoAdjuntoRepository documentoAdjuntoRepository)
        {
            var (ruta, nombreFisico) = await fileStorageService.SaveAsync(file, $"actividades/{actividadId}"
        );

            var documento = new DocumentoAdjunto
            {
                ActividadID = actividadId,
                NombreOriginal = file.FileName,
                NombreFisico = nombreFisico,
                Ruta = ruta,
                TipoMime = file.ContentType,
                Tamano = file.Length,
                FechaSubida = DateTime.Now,
                UsuarioID = usuarioId
            };

            await documentoAdjuntoRepository.AddAsync(documento);
        }
    }
}
