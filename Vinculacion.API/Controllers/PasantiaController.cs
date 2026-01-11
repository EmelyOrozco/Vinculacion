using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Application.Services;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasantiaController : ControllerBase
    {
        private readonly IPasantiaService _pasantiaService;

        public PasantiaController(IPasantiaService pasantiaService)
        {
            _pasantiaService = pasantiaService;
        }

        /// <summary>
        /// Obtiene los titulos de pasantias a seleccionar para Subida
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPasantiasActivasToSubida()
        {
            var pasantias = await _pasantiaService.GetPasantiasActivasFinalizadas();

            if (pasantias == null || !pasantias.Any())
            {
                return NotFound("No se encontraron pasantías activas o finalizadas.");
            }

            return Ok(pasantias);
        }

        /// <summary>
        /// Obtiene todas las pasantias subidas
        /// </summary>
        [HttpGet("activas-subida")]
        public async Task<IActionResult> GetAllPasantiaVinculacion()
        {
            var result = await _pasantiaService.GetAllPasantiaVinculacion();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
