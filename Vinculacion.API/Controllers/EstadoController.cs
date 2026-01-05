using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet("{tabla}")]
        public async Task<IActionResult> GetByTabla(string tabla)
        {
            var result = await _estadoService.GetEstadosPorTablaAsync(tabla);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _estadoService.GetAllAsync();
            return Ok(result);
        }
    }
}
