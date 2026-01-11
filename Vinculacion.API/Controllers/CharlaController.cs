using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharlaController : ControllerBase
    {
        private readonly ICharlaService _charlaService;
        public CharlaController(ICharlaService charlaService)
        {
            _charlaService = charlaService;
        }

        /// <summary>
        /// Obtiene los titulos de charlas a seleccionar para Subida
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCharlasActivasToSubida() 
        {
            var charla = await _charlaService.GetCharlasActivasFinalizadas();

            if (charla == null || !charla.Any())
            {
                return NotFound("No se encontraron pasantías activas o finalizadas.");
            }

            return Ok(charla);
        }

        /// <summary>
        /// Obtiene todas las charlas subidas
        /// </summary>
        [HttpGet("activas-subida")]
        public async Task<IActionResult> GetAllCharlaVinculacion()
        {
            var result = await _charlaService.GetAllCharlaVinculacion();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }   
    }
}
