using FluentValidation;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Extentions.ActorExternoExtentions;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Services.ActorExterno;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services.ActorExterno
{
    public class ActorPersonaService: IActorPersonaService
    {
        private readonly IActorPersonaRepository _actorPersonaRepository;
        private readonly IActorExternoRepository _actorExternoRepository;
        private readonly IValidator<AddActorPersonaDto> _validator;
        private readonly IValidator<AddActorExternoDto> _validatorExterno;
        private readonly IPaisRepository _paisRepository;

        public ActorPersonaService(
            IActorPersonaRepository actorPersonaRepository,
            IActorExternoRepository actorExternoRepository,
            IValidator<AddActorPersonaDto> validator, IValidator<AddActorExternoDto> validatorExterno, IPaisRepository paisRepository)
        {
            _actorPersonaRepository = actorPersonaRepository;
            _actorExternoRepository = actorExternoRepository;
            _validator = validator;
            _validatorExterno = validatorExterno;
            _paisRepository = paisRepository;
        }

        public async Task<OperationResult<AddActorPersonaDto>> AddActorPersonaAsync(AddActorPersonaDto addActorPersonaDto, AddActorExternoDto addActorExternoDto)
        {

            var validationActorPersona = await _validator.ValidateAsync(addActorPersonaDto);
            if (!validationActorPersona.IsValid)
            {
                return OperationResult<AddActorPersonaDto>.Failure("Error: ", validationActorPersona.Errors.Select(x => x.ErrorMessage));
            }

            var validationActorExterno = await _validatorExterno.ValidateAsync(addActorExternoDto);
            if (!validationActorExterno.IsValid)
            {
                return OperationResult<AddActorPersonaDto>.Failure("Error: ", validationActorExterno.Errors.Select(x => x.ErrorMessage));
            }

            if (await _paisRepository.PaisExists(addActorPersonaDto.PaisID))
            {
                return OperationResult<AddActorPersonaDto>.Failure("El país seleccionado no existe", null);
            }
 
            var actorExternoEntity = addActorExternoDto.ToActorExternoToDto();
            var resultActor = await _actorExternoRepository.AddAsync(actorExternoEntity);

            var entity = addActorPersonaDto.ToActorPersonaFromActorPersonaDto();
            entity.ActorExternoID = actorExternoEntity.ActorExternoID;
            var resultPersona = await _actorPersonaRepository.AddAsync(entity);

            return OperationResult<AddActorPersonaDto>.Success("Persona Vinculante añadida correctamente", resultPersona);
        }

    }
}
