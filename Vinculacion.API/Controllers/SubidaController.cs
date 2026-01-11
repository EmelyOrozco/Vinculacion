using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos;
using Vinculacion.Application.Enums;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubidaController : ControllerBase
    {
        private readonly ISubidaService _subidaService;
        public SubidaController(ISubidaService subidaService)
        {
            _subidaService = subidaService;
        }

        /// <summary>
        /// Subida a partir de un excel
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("ejecutar")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EjecutarSubida([FromForm] SubidaExcelRequest request)
        {
            if (request.Archivo is null || request.Archivo.Length == 0)
            {
                return BadRequest("Debe adjuntar un archivo Excel");
            }

            await using var stream = request.Archivo.OpenReadStream();

            await _subidaService.EjecutarSubida(request.TipoSubida, request.ContextoId,stream, HttpContext.RequestAborted);

            return Ok("Subida ejecutada correctamente.");
        }


        /// <summary>
        /// Descarga plantilla Excel para subida.
        /// </summary>
        [HttpGet("plantilla")]
        public async Task<IActionResult> DescargarPlantillaExcel(CancellationToken cancellationToken, TipoSubida tipoSubida)
        {
            var archivo = await _subidaService.GenerarPlantillaExcel(tipoSubida,cancellationToken);

            return File(archivo.Contenido, archivo.ContentType, archivo.NombreArchivo);

            //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}
