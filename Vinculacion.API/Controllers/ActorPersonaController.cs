using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Features.ActorVinculacion.Commands;
using Vinculacion.Domain.Base;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorPersonaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActorPersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActorPersonaCommand command)
        {
            try {
                if (command is null)
                {
                    return BadRequest("La solicitud no puede ser nula");
                }
                if (string.IsNullOrWhiteSpace(command.ActorPersona.NombreCompleto))
                {
                    return BadRequest("El nombre es obligatorio");
                }
                var result = await _mediator.Send(command);
                return Ok(OperationResult<int>.Success("Registro exitoso",result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OperationResult<string>.Failure(ex.Message));
            }
        } 
    
    }
}
