using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
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
            return Ok(result.Data);
        }

        [HttpGet]
        public async  Task<IActionResult> GetActorPersona()
        {
            var result = await _actorPersonaService.GetActorPersonaAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActorPersonaById(decimal id)
        {
            var result = await _actorPersonaService.GetActorPersonaById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActorPersona(decimal id,[FromBody] UpdateActorPersonaDto dto)
        {
            var result = await _actorPersonaService
                .UpdateActorPersonaAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}