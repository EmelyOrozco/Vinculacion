using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;

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

        [Authorize(Roles = "Superusuario, Usuario Consultor")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _actividadVinculacionService.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize(Roles = "Superusuario, Usuario Consultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var result = await _actividadVinculacionService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize(Roles = "Superusuario, Usuario Final")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] ActividadVinculacionDto dto)
        {
            var result = await _actividadVinculacionService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

    }
}
