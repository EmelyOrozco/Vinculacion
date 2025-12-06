using FluentValidation;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Extentions.ActorExternoExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActorExternoService
{
    public class ActorPersonaService: IActorPersonaService
    {
        private readonly IActorPersonaRepository _actorPersonaRepository;
        private readonly IActorExternoRepository _actorExternoRepository;
        private readonly IValidator<AddActorPersonaDto> _validator;
        private readonly IPaisRepository _paisRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActorPersonaService(
            IActorPersonaRepository actorPersonaRepository,
            IActorExternoRepository actorExternoRepository,
            IValidator<AddActorPersonaDto> validator,
            IPaisRepository paisRepository,
            IUnitOfWork unitOfWork)
        {
            _actorPersonaRepository = actorPersonaRepository;
            _actorExternoRepository = actorExternoRepository;
            _validator = validator;
            _paisRepository = paisRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<AddActorPersonaDto>> AddActorPersonaAsync(AddActorPersonaDto addActorPersonaDto)
        {

            var validationActorPersona = await _validator.ValidateAsync(addActorPersonaDto);
            if (!validationActorPersona.IsValid)
            {
                return OperationResult<AddActorPersonaDto>.Failure("Error: ", validationActorPersona.Errors.Select(x => x.ErrorMessage));
            }

            //var validationActorExterno = await _validatorExterno.ValidateAsync(addActorExternoDto);
            //if (!validationActorExterno.IsValid)
            //{
            //    return OperationResult<AddActorPersonaDto>.Failure("Error: ", validationActorExterno.Errors.Select(x => x.ErrorMessage));
            //}

            if (await _paisRepository.PaisExists(addActorPersonaDto.PaisID))
            {
                return OperationResult<AddActorPersonaDto>.Failure("El país seleccionado no existe", null);
            }

            var actorExternoEntity = new ActorExterno
            {
                TipoActorID = 2,
                EstatusID = 1,
            };

            //_addActorExternoDto.ToActorExternoToDto();
            //actorExternoEntity.TipoActorID = 2; 
            //actorExternoEntity.EstatusID = 1;
            var resultActor = await _actorExternoRepository.AddAsync(actorExternoEntity);

            var entity = addActorPersonaDto.ToActorPersonaFromActorPersonaDto();
            entity.ActorExternoID = actorExternoEntity.ActorExternoID;
            var resultPersona = await _actorPersonaRepository.AddAsync(entity);

            var result = await _unitOfWork.SaveChangesAsync();
            return OperationResult<AddActorPersonaDto>.Success("Persona Vinculante añadida correctamente", result);
        }

    }
}
