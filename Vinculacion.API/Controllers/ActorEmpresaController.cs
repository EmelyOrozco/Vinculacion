using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorEmpresaController : ControllerBase
    {
        private readonly IActorEmpresaService _actorEmpresaService;
        public ActorEmpresaController(IActorEmpresaService actorEmpresaService)
        {
            _actorEmpresaService = actorEmpresaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActorEmpresa([FromBody] AddActorEmpresaDto addActorEmpresaDto)
        {
            var result = await _actorEmpresaService.AddActorEmpresaAsync(addActorEmpresaDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

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
    }
}