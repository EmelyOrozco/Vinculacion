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
        public ActorPersonaService(ILogger<ActorPersonaService> logger, IActorPersonaRepository actorPersonaRepository)
        {
            _logger = logger;
            _actorPersonaRepository = actorPersonaRepository;
        }

        public async Task<OperationResult<CreateActorPersonaDto>> AddActorPersonaAsync(CreateActorPersonaDto createActorPersonaDto)
        {
            var entity = createActorPersonaDto.ToActorPersonaFromActorPersonaDto();
            var result = await _actorPersonaRepository.AddAsync(entity);
            return OperationResult<CreateActorPersonaDto>.Success("Persona Vinculante creada correctamente",result);
        }

    }
}
