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

            if (!await _paisRepository.PaisExists(addActorPersonaDto.PaisID))
            {
                return OperationResult<AddActorPersonaDto>.Failure("El país seleccionado no existe", null);
            }

            var actorExternoEntity = new ActorExterno
            {
                TipoActorID = 2,
                EstadoID = 1,
                FechaRegistro = DateTime.Now
            };

            var resultActor = await _actorExternoRepository.AddAsync(actorExternoEntity);
            await _unitOfWork.SaveChangesAsync();

            var entity = addActorPersonaDto.ToActorPersonaFromActorPersonaDto();
            entity.ActorExternoID = actorExternoEntity.ActorExternoID;
            var resultPersona = await _actorPersonaRepository.AddAsync(entity);

            var result = await _unitOfWork.SaveChangesAsync();
            return OperationResult<AddActorPersonaDto>.Success("Persona Vinculante añadida correctamente", resultPersona.Data);
        }


        public async Task<OperationResult<List<AddActorPersonaDto>>> GetActorPersonaAsync()
        {
            var actorPersona = await _actorPersonaRepository.GetAllAsync(l => true);
            if (!actorPersona.IsSuccess)
            {
                return OperationResult<List<AddActorPersonaDto>>.Failure($"Error obteniendo las personas vinculadas {actorPersona.Message}");
            }

            return OperationResult<List<AddActorPersonaDto>>.Success("Personas vinculadas: ", actorPersona.Data);
        }

        public async Task<OperationResult<AddActorPersonaDto>> GetActorPersonaById(decimal id)
        {
            var actorPersona = await _actorPersonaRepository.GetByIdAsync(id);
            if (!actorPersona.IsSuccess)
            {
                return OperationResult<AddActorPersonaDto>.Failure($"Error la persona vinculada no existe {actorPersona.Message}");
            }

            return OperationResult<AddActorPersonaDto>.Success("Persona vinculada: ", actorPersona.Data);
        }

    }
}