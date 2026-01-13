using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;

namespace Vinculacion.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ActividadSubtareasController : ControllerBase
    {
        private readonly IActividadSubtareasService _service;

        public ActividadSubtareasController(IActividadSubtareasService service)
        {
            _service = service;
        }

        [HttpGet("actividades/{actividadId:decimal}/subtareas")]
        public async Task<IActionResult> GetSubtarea(decimal actividadId)
            => Ok(await _service.GetByActividadAsync(actividadId));

        [HttpPost("actividades/{actividadId:decimal}/subtareas")]
        public async Task<IActionResult> CreateSubtarea(decimal actividadId, ActividadSubtareaCreateDto dto)
            => Ok(await _service.CreateAsync(actividadId, dto));

        [HttpPut("subtareas/{subtareaId:decimal}")]
        public async Task<IActionResult> UpdateSubtarea(decimal subtareaId, ActividadSubtareaUpdateDto dto)
            => Ok(await _service.UpdateAsync(subtareaId, dto));
    }

}
