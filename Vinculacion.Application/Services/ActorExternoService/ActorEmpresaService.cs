using FluentValidation;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Extentions.ActorExternoExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActorExternoService
{
    public class ActorEmpresaService: IActorEmpresaService
    {
        private readonly IActorEmpresaRepository _actorEmpresaRepository;
        private readonly IActorExternoRepository _actorExternoRepository;
        private readonly IActorEmpresaClasificacionRepository _actorEmpresaClasificacionRepository;
        private readonly IValidator<AddActorEmpresaDto> _validator;
        private readonly IPaisRepository _paisRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ActorEmpresaService(
            IActorEmpresaRepository actorEmpresaRepository,
            IActorExternoRepository actorExternoRepository,
            IActorEmpresaClasificacionRepository actorEmpresaClasificacionRepository,
            IValidator<AddActorEmpresaDto> validator,
            IPaisRepository paisRepository,
            IUnitOfWork unitOfWork)
        {
            _actorEmpresaRepository = actorEmpresaRepository;
            _actorExternoRepository = actorExternoRepository;
            _actorEmpresaClasificacionRepository = actorEmpresaClasificacionRepository;
            _validator = validator;
            _paisRepository = paisRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<AddActorEmpresaDto>> AddActorEmpresaAsync(AddActorEmpresaDto addActorEmpresaDto)
        {
            var validationActorEmpresa = await _validator.ValidateAsync(addActorEmpresaDto);
            if (!validationActorEmpresa.IsValid)
            {
                return OperationResult<AddActorEmpresaDto>.Failure(
                    "Error:",
                    validationActorEmpresa.Errors.Select(x => x.ErrorMessage));
            }

            if (addActorEmpresaDto.Clasificaciones == null ||
                !addActorEmpresaDto.Clasificaciones.Any())
            {
                return OperationResult<AddActorEmpresaDto>.Failure(
                    "Debe seleccionar al menos una clasificación",
                    null);
            }

            if (!await _paisRepository.PaisExists(addActorEmpresaDto.PaisID))
            {
                return OperationResult<AddActorEmpresaDto>.Failure(
                    "El país seleccionado no existe",
                    null);
            }

            var actorExternoEntity = new ActorExterno
            {
                TipoActorID = 1,
                EstadoID = 1,
                FechaRegistro = DateTime.Now
            };

            await _actorExternoRepository.AddAsync(actorExternoEntity);
            await _unitOfWork.SaveChangesAsync();

            var entity = addActorEmpresaDto.ToActorEmpresaFromActorEmpresaDto();
            entity.ActorExternoID = actorExternoEntity.ActorExternoID;
            await _actorEmpresaRepository.AddAsync(entity);

            var clasificaciones = addActorEmpresaDto
                .ToActorEmpresaClasificaciones(actorExternoEntity.ActorExternoID);

            foreach (var clasificacion in clasificaciones)
            {
                await _actorEmpresaClasificacionRepository.AddAsync(clasificacion);
            }

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<AddActorEmpresaDto>.Success(
                "Empresa Vinculante añadida correctamente",
                addActorEmpresaDto);
        }

        public async Task<OperationResult<List<ActorEmpresaResponseDto>>> GetActorEmpresaAsync()
        {
            var entities = await _actorEmpresaRepository.GetAllWithClasificacionesAsync();

            if (!entities.Any())
            {
                return OperationResult<List<ActorEmpresaResponseDto>>
                    .Failure("No existen empresas registradas", null);
            }

            var result = entities.Select(e => e.ToResponseDto()).ToList();

            return OperationResult<List<ActorEmpresaResponseDto>>.Success("Empresas obtenidas correctamente", result);
        }
        public async Task<OperationResult<ActorEmpresaResponseDto>> GetActorEmpresaById(decimal id)
        {
            var entity = await _actorEmpresaRepository.GetByIdWithClasificacionesAsync(id);

            if (entity == null)
            {
                return OperationResult<ActorEmpresaResponseDto>
                    .Failure("La empresa no existe", null);
            }

            return OperationResult<ActorEmpresaResponseDto>
                .Success("Empresa obtenida correctamente", entity.ToResponseDto());
        }

    }
}