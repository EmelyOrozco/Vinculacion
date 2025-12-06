using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Services.ActorExterno;

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

        public async Task<IActionResult> CreateActorPersona([FromBody] object request)
        {
            return Ok();
        }

    }
}
