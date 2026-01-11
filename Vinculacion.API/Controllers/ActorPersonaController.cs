using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorPersonaController : BaseController
    {
        private readonly IActorPersonaService _actorPersonaService;
        public ActorPersonaController(IActorPersonaService actorPersonaService)
        {
            _actorPersonaService = actorPersonaService;
        }

        [Authorize(Roles = "Superusuario")]
        [HttpPost]
        public async Task<IActionResult> CreateActorPersona([FromBody] AddActorPersonaDto addActorPersonaDto)
        {
            var usuarioId = UsuarioId;
            var result = await _actorPersonaService.AddActorPersonaAsync(addActorPersonaDto, usuarioId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [Authorize(Roles = "Superusuario, Usuario Consultor")]
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

        [Authorize(Roles = "Superusuario, Usuario Consultor")]
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

        [Authorize(Roles = "Superusuario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActorPersona(decimal id,[FromBody] UpdateActorPersonaDto dto)
        {
            var usuarioId = UsuarioId;
            var result = await _actorPersonaService.UpdateActorPersonaAsync(id, dto, usuarioId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}