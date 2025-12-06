using Microsoft.Extensions.Logging;
using Vinculacion.Application.Extentions;
using Vinculacion.Application.Features.ActorVinculacion.Dtos;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services
{
    public class ActorPersonaService: IActorPersonaService
    {
        private readonly ILogger<ActorPersonaService> _logger;
        private readonly IActorPersonaRepository _actorPersonaRepository;
        private readonly IActorExternoRepository _actorExternoRepository;
        public ActorPersonaService(ILogger<ActorPersonaService> logger, IActorPersonaRepository actorPersonaRepository, IActorExternoRepository actorExternoRepository)
        {
            _logger = logger;
            _actorPersonaRepository = actorPersonaRepository;
            _actorExternoRepository = actorExternoRepository;
        }

        public async Task<OperationResult<CreateActorPersonaDto>> AddActorPersonaAsync(CreateActorPersonaDto createActorPersonaDto, AddActorExternoDto addActorExternoDto)
        {
            var entity = createActorPersonaDto.ToActorPersonaFromActorPersonaDto();
            var actorExternoEntity = addActorExternoDto.ToActorExternoToDto();

            var ActorExternoID = entity.ActorExternoID;

            var result = await _actorPersonaRepository.AddAsync(entity);

            var ActorExterno = await _actorExternoRepository.AddAsync(actorExternoEntity);

            return OperationResult<CreateActorPersonaDto>.Success("Persona Vinculante añadida correctamente",result);
        }

    }
}
