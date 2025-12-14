using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadVinculacionController : ControllerBase
    {
        private readonly IActividadVinculacionService _actividadVinculacionService;
        public ActividadVinculacionController(IActividadVinculacionService actividadVinculacionService)
        {
            _actividadVinculacionService = actividadVinculacionService;
        }

        [Authorize(Roles = "Superusuario, Usuario Final")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActividadVinculacionDto actividadVinculacionDto)
        {
            var result = await _actividadVinculacionService.AddActividadVinculacion(actividadVinculacionDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result.Message);
        }

    }
}
