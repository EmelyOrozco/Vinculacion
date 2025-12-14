using FluentValidation;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
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
            return OperationResult<AddActorPersonaDto>.Success("Persona Vinculante añadida correctamente", addActorPersonaDto);
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

        public async Task<OperationResult<bool>> UpdateActorPersonaAsync(decimal id, UpdateActorPersonaDto dto)
        {
            var entity = await _actorPersonaRepository.GetByIdWithActorExternoAsync(id);

            if (entity == null)
                return OperationResult<bool>.Failure("La persona no existe");

            if (entity.ActorExterno == null)
                return OperationResult<bool>.Failure("Error de integridad: ActorExterno no existe");

            if (!string.IsNullOrWhiteSpace(dto.NombreCompleto))
                entity.NombreCompleto = dto.NombreCompleto;

            if (dto.TipoIdentificacion.HasValue && dto.TipoIdentificacion > 0)
                entity.TipoIdentificacion = dto.TipoIdentificacion.Value;

            if (!string.IsNullOrWhiteSpace(dto.IdentificacionNumero))
                entity.IdentificacionNumero = dto.IdentificacionNumero;

            if (!string.IsNullOrWhiteSpace(dto.Correo))
                entity.Correo = dto.Correo;

            if (!string.IsNullOrWhiteSpace(dto.Telefono))
                entity.Telefono = dto.Telefono;

            if (dto.Sexo.HasValue && dto.Sexo > 0)
                entity.Sexo = dto.Sexo.Value;

            if (dto.PaisID.HasValue && dto.PaisID > 0)
                entity.PaisID = dto.PaisID.Value;

            if (dto.EstadoID.HasValue && dto.EstadoID > 0)
                entity.ActorExterno.EstadoID = dto.EstadoID.Value;

            entity.ActorExterno.FechaModificacion = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success("Persona actualizada correctamente", true);
        }
    }
}