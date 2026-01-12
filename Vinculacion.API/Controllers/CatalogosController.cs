using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Services.ICatalogoService;

namespace Vinculacion.API.Controllers
{
    [ApiController]
    [Route("api/catalogos")]
    public class CatalogosController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogosController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet("paises")]
        public async Task<IActionResult> GetPaises()
        {
            return Ok(await _catalogoService.GetPaisesAsync());
        }

        [HttpGet("recintos")]
        public async Task<IActionResult> GetRecintos()
        {
            return Ok(await _catalogoService.GetRecintosAsync());
        }

        [HttpGet("facultades")]
        public async Task<IActionResult> GetFacultades()
        {
            return Ok(await _catalogoService.GetFacultadesAsync());
        }

        [HttpGet("escuelas")]
        public async Task<IActionResult> GetEscuelas([FromQuery] decimal facultadId)
        {
            return Ok(await _catalogoService.GetEscuelasByFacultadAsync(facultadId));
        }

        [HttpGet("carreras")]
        public async Task<IActionResult> GetCarreras([FromQuery] decimal escuelaId)
        {
            return Ok(await _catalogoService.GetCarrerasByEscuelaAsync(escuelaId));
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _catalogoService.GetRolesAsync());
        }

        [HttpGet("clasificaciones-empresa")]
        public async Task<IActionResult> GetClasificacionesEmpresa()
        {
            return Ok(await _catalogoService.GetClasificacionesEmpresaAsync());
        }

        [HttpGet("tipos-persona")]
        public async Task<IActionResult> GetTiposPersona()
        {
            return Ok(await _catalogoService.GetTiposPersonaAsync());
        }
    }

}
