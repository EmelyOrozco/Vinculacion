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
    }
}
