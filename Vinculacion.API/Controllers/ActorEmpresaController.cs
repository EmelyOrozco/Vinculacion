using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorEmpresaController : BaseController
    {
        private readonly IActorEmpresaService _actorEmpresaService;
        public ActorEmpresaController(IActorEmpresaService actorEmpresaService)
        {
            _actorEmpresaService = actorEmpresaService;
        }

        [Authorize(Roles = "Superusuario")]
        [HttpPost]
        public async Task<IActionResult> CreateActorEmpresa([FromBody] AddActorEmpresaDto addActorEmpresaDto)
        {
            var usuarioId = UsuarioId;
            var result = await _actorEmpresaService.AddActorEmpresaAsync(addActorEmpresaDto, usuarioId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [Authorize(Roles = "Superusuario, Usuario Final, Usuario Consultor")]
        [HttpGet]
        public async Task<IActionResult> GetActorEmpresa()
        {
            var result = await _actorEmpresaService.GetActorEmpresaAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
        [Authorize(Roles = "Superusuario, Usuario Final, Usuario Consultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActorEmpresaById(decimal id)
        {
            var result = await _actorEmpresaService.GetActorEmpresaById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
        [Authorize(Roles = "Superusuario")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActorEmpresa(decimal id,[FromBody] UpdateActorEmpresaDto dto)
        {
            var usuarioId = UsuarioId;
            var result = await _actorEmpresaService.UpdateActorEmpresaAsync(id, dto, usuarioId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}