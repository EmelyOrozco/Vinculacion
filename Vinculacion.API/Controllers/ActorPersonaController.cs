using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorPersonaController : ControllerBase
    {
        private readonly IActorPersonaService _actorPersonaService;
        public ActorPersonaController(IActorPersonaService actorPersonaService)
        {
            _actorPersonaService = actorPersonaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActorPersona([FromBody] AddActorPersonaDto addActorPersonaDto)
        {
            var result = await _actorPersonaService.AddActorPersonaAsync(addActorPersonaDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

    }
}
