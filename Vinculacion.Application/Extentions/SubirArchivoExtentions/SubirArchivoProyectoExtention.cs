using Microsoft.AspNetCore.Http;
using Vinculacion.Application.Interfaces.Repositories.DocumentoAdjuntoRepository;
using Vinculacion.Application.Interfaces.Services.IFileStorageService;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.SubirArchivoExtentions
{
    public static class SubirArchivoProyectoExtensions
    {
        public static async Task SubirArchivoProyectoExtention(decimal proyectoId, IFormFile file,
                                                                decimal usuarioId, IFileStorageService fileStorageService,
                                                                IDocumentoAdjuntoRepository documentoAdjuntoRepository)
        {
            var (ruta, nombreFisico) = await fileStorageService.SaveAsync(file, $"proyectos/{proyectoId}"
        );

            var documento = new DocumentoAdjunto
            {
                ProyectoID = proyectoId,
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
