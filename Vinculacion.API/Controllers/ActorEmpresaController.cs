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
    }
}
