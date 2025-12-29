using Microsoft.AspNetCore.Mvc;
using Vinculacion.API.Models;
using Vinculacion.Application.Extentions.SubirArchivoExtentions;
using Vinculacion.Application.Interfaces.Repositories.DocumentoAdjuntoRepository;
using Vinculacion.Application.Interfaces.Services.IFileStorageService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IDocumentoAdjuntoRepository _documentoAdjuntoRepository;
        private readonly IDocumentoAdjuntoService _documentoAdjuntoService;

        public ArchivoController(
            IFileStorageService fileStorageService,
            IDocumentoAdjuntoRepository documentoAdjuntoRepository,
            IDocumentoAdjuntoService documentoAdjuntoService)
        {
            _fileStorageService = fileStorageService;
            _documentoAdjuntoRepository = documentoAdjuntoRepository;
            _documentoAdjuntoService = documentoAdjuntoService;
        }

        [HttpPost("actividades/{actividadId}/archivos")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirArchivoActividad(decimal actividadId, [FromForm] SubirArchivoRequest request)
        {
            var usuarioId = 1;

            await SubirArchivoActividadExtensions.SubirArchivoActividadExtention(
                actividadId,
                request.File,
                usuarioId,
                _fileStorageService,
                _documentoAdjuntoRepository
            );

            return Ok("Archivo subido correctamente");
        }

        [HttpPost("proyectos/{proyectoId}/archivos")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirArchivoProyecto(decimal proyectoId,[FromForm] SubirArchivoRequest request)
        {
            var usuarioId = 1;

            await SubirArchivoProyectoExtensions.SubirArchivoProyectoExtention(
                proyectoId,
                request.File,
                usuarioId,
                _fileStorageService,
                _documentoAdjuntoRepository
            );

            return Ok("Archivo de proyecto subido correctamente");
        }

        [HttpGet("actividades/{actividadId}")]
        public async Task<IActionResult> ObtenerArchivosActividad(decimal actividadId)
        {
            var archivos = await _documentoAdjuntoService
                .ObtenerArchivosActividadAsync(actividadId);

            return Ok(archivos);
        }

        [HttpGet("proyectos/{proyectoId}")]
        public async Task<IActionResult> ObtenerArchivosProyecto(decimal proyectoId)
        {
            var archivos = await _documentoAdjuntoService
                .ObtenerArchivosProyectoAsync(proyectoId);

            return Ok(archivos);
        }

    }
}
