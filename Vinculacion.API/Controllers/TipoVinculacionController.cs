using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVinculacionController : ControllerBase
    {
        private readonly ITipoVinculacionService _tipoVinculacionService;

        public TipoVinculacionController(ITipoVinculacionService tipoVinculacionService)
        {
            _tipoVinculacionService = tipoVinculacionService;
        }

        [HttpGet("proyectos")]
        public async Task<IActionResult> GetTiposProyecto()
        {
            var tipos = await _tipoVinculacionService.GetTiposProyectoAsync();
            return Ok(tipos);
        }
    }
}
