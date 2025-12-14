using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaVinculacionController : ControllerBase
    {
        private readonly IPersonaVinculacionService _personaVinculacionService;
        public PersonaVinculacionController(IPersonaVinculacionService personaVinculacionService)
        {
            _personaVinculacionService = personaVinculacionService;
        }

        [Authorize(Roles = "Superusuario, Usuario Final")]
        [HttpPost]
        public async Task<IActionResult> AddPersonaVinculacion([FromBody] PersonaVinculacionDto request)
        {
            var result = await _personaVinculacionService.AddPersonaVinculacion(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result.Data);
        }

        [Authorize(Roles = "Superusuario, Usuario Consultor")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personaVinculacionService.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize(Roles = "Superusuario, Usuario Consultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var result = await _personaVinculacionService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [Authorize(Roles = "Superusuario, Usuario Final")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] PersonaVinculacionDto dto)
        {
            var result = await _personaVinculacionService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
