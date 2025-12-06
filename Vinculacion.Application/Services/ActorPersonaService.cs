using FluentValidation;
using Microsoft.Extensions.Logging;
using Vinculacion.Application.Extentions;
using Vinculacion.Application.Features.ActorVinculacion.Dtos;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services
{
    public class ActorPersonaService: IActorPersonaService
    {
        private readonly ILogger<ActorPersonaService> _logger;
        private readonly IActorPersonaRepository _actorPersonaRepository;
        private readonly IActorExternoRepository _actorExternoRepository;
        private readonly IValidator<CreateActorPersonaDto> _validator;
        public ActorPersonaService(ILogger<ActorPersonaService> logger, IActorPersonaRepository actorPersonaRepository, IActorExternoRepository actorExternoRepository, IValidator<CreateActorPersonaDto> validator)
        {
            _logger = logger;
            _actorPersonaRepository = actorPersonaRepository;
            _actorExternoRepository = actorExternoRepository;
            _validator = validator;
        }

        public async Task<OperationResult<CreateActorPersonaDto>> AddActorPersonaAsync(CreateActorPersonaDto createActorPersonaDto, AddActorExternoDto addActorExternoDto)
        {

            var validationActorPersona = await _validator.ValidateAsync(createActorPersonaDto);
            if (!validationActorPersona.IsValid)
            {
                return OperationResult<CreateActorPersonaDto>.Failure("Error: ", validationActorPersona.Errors.Select(x => x.ErrorMessage));
            }

            var actorExternoEntity = addActorExternoDto.ToActorExternoToDto();
            var resultActor = await _actorExternoRepository.AddAsync(actorExternoEntity);

            var entity = createActorPersonaDto.ToActorPersonaFromActorPersonaDto();
            entity.ActorExternoID = actorExternoEntity.ActorExternoID;
            var resultPersona = await _actorPersonaRepository.AddAsync(entity);

            return OperationResult<CreateActorPersonaDto>.Success("Persona Vinculante añadida correctamente", resultPersona);
        }

    }
}
