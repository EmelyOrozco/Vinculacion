using FluentValidation;
using System.Text.Json;
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

        public async Task<OperationResult<AddActorEmpresaDto>> AddActorEmpresaAsync(AddActorEmpresaDto addActorEmpresaDto, decimal usuarioId)
        {
            bool actorEmpresaExists = await _actorEmpresaRepository.ActorEmpresaExistsAsync(addActorEmpresaDto.IdentificacionNumero, addActorEmpresaDto.NombreEmpresa);

            if (actorEmpresaExists)  
            {
                return OperationResult<AddActorEmpresaDto>.Failure("Esta empresa se encuentra registrada");
            }

            var validationActorEmpresa = await _validator.ValidateAsync(addActorEmpresaDto);
            if (!validationActorEmpresa.IsValid)
            {
                return OperationResult<AddActorEmpresaDto>.Failure("Error:",validationActorEmpresa.Errors.Select(x => x.ErrorMessage));
            }

            if (addActorEmpresaDto.Clasificaciones == null || !addActorEmpresaDto.Clasificaciones.Any())
            {
                return OperationResult<AddActorEmpresaDto>.Failure("Debe seleccionar al menos una clasificación",null);
            }

            if (addActorEmpresaDto.TipoIdentificacion != 0 && addActorEmpresaDto.TipoIdentificacion is not null)
            {
                bool validarIdentificacion = FuncionesService.ValidarIdentificacion(addActorEmpresaDto.TipoIdentificacion, addActorEmpresaDto.IdentificacionNumero);

                if (!validarIdentificacion)
                {
                    return OperationResult<AddActorEmpresaDto>.Failure("El no. de identificacion no es valido");
                }
            }

            if (!await _paisRepository.PaisExists(addActorEmpresaDto.PaisID))
            {
                return OperationResult<AddActorEmpresaDto>.Failure("El país seleccionado no existe",null);
            }

            var actorExternoEntity = new ActorExterno
            {
                TipoActorID = 1,
                EstadoID = 1,
                FechaRegistro = DateTime.Now
            };

            await _actorExternoRepository.AddAsync(actorExternoEntity);
            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Crear",
                Entidad = "ActorEmpresa",
                EntidadId = null
            });
            await _unitOfWork.SaveChangesAsync();

            var entity = addActorEmpresaDto.ToActorEmpresaFromActorEmpresaDto();
            entity.ActorExternoID = actorExternoEntity.ActorExternoID;
            await _actorEmpresaRepository.AddAsync(entity);

            var clasificaciones = addActorEmpresaDto.ToActorEmpresaClasificaciones(actorExternoEntity.ActorExternoID);

            foreach (var clasificacion in clasificaciones)
            {
                await _actorEmpresaClasificacionRepository.AddAsync(clasificacion);
            }

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<AddActorEmpresaDto>.Success("Empresa Vinculante añadida correctamente",addActorEmpresaDto);
        }

        public async Task<OperationResult<List<ActorEmpresaResponseDto>>> GetActorEmpresaAsync()
        {
            var entities = await _actorEmpresaRepository.GetAllWithClasificacionesAsync();

            if (!entities.Any())
            {
                return OperationResult<List<ActorEmpresaResponseDto>>.Failure("No existen empresas registradas", null);
            }

            var result = entities.Select(e => e.ToResponseDto()).ToList();

            return OperationResult<List<ActorEmpresaResponseDto>>.Success("Empresas obtenidas correctamente", result);
        }

        public async Task<OperationResult<ActorEmpresaResponseDto>> GetActorEmpresaById(decimal id)
        {
            var entity = await _actorEmpresaRepository.GetByIdWithClasificacionesAsync(id);

            if (entity == null)
            {
                return OperationResult<ActorEmpresaResponseDto>.Failure("La empresa no existe", null);
            }

            return OperationResult<ActorEmpresaResponseDto>.Success("Empresa obtenida correctamente", entity.ToResponseDto());
        }

        public async Task<OperationResult<bool>> UpdateActorEmpresaAsync(decimal id, UpdateActorEmpresaDto dto, decimal usuarioId)
        {
            var entity = await _actorEmpresaRepository.GetByIdWithClasificacionesAsync(id);

            if (entity == null)
                return OperationResult<bool>.Failure("La empresa no existe");

            var antes = new
            {
                entity.NombreEmpresa,
                entity.IdentificacionNumero,
                entity.ContactoNombrePersona,
                entity.ContactoCorreo,
                entity.ContactoTelefono,
                entity.PaisID,
                entity.ActorExterno.EstadoID,
                Clasificaciones = entity.ActorEmpresaClasificaciones
                .Select(x => x.ClasificacionID)
                .ToList()
            };
           
            if (entity.ActorExterno == null)
                return OperationResult<bool>.Failure("Error de integridad: ActorExterno no existe");

            if (!string.IsNullOrWhiteSpace(dto.NombreEmpresa))
                entity.NombreEmpresa = dto.NombreEmpresa;

            if (dto.TipoIdentificacion.HasValue && dto.TipoIdentificacion > 0)
                entity.TipoIdentificacion = dto.TipoIdentificacion.Value;

            if (!string.IsNullOrWhiteSpace(dto.IdentificacionNumero))
                entity.IdentificacionNumero = dto.IdentificacionNumero;

            if (!string.IsNullOrWhiteSpace(dto.ContactoNombrePersona))
                entity.ContactoNombrePersona = dto.ContactoNombrePersona;

            if (!string.IsNullOrWhiteSpace(dto.ContactoCorreo))
                entity.ContactoCorreo = dto.ContactoCorreo;

            if (!string.IsNullOrWhiteSpace(dto.ContactoTelefono))
                entity.ContactoTelefono = dto.ContactoTelefono;

            if (dto.ContactoSexoPersona.HasValue && dto.ContactoSexoPersona > 0)
                entity.ContactoSexoPersona = dto.ContactoSexoPersona.Value;

            if (dto.PaisID.HasValue && dto.PaisID > 0)
                entity.PaisID = dto.PaisID.Value;

            if (dto.EstadoID.HasValue && dto.EstadoID > 0)
                entity.ActorExterno.EstadoID = dto.EstadoID.Value;

            entity.ActorExterno.FechaModificacion = DateTime.Now;

            if (dto.Clasificaciones != null)
            {
                entity.ActorEmpresaClasificaciones.Clear();

                foreach (var clasificacionId in dto.Clasificaciones)
                {
                    entity.ActorEmpresaClasificaciones.Add(
                        new ActorEmpresaClasificacion
                        {
                            ActorExternoID = id,
                            ClasificacionID = clasificacionId
                        });
                }
            }

            var despues = new
            {
                entity.NombreEmpresa,
                entity.IdentificacionNumero,
                entity.ContactoNombrePersona,
                entity.ContactoCorreo,
                entity.ContactoTelefono,
                entity.PaisID,
                entity.ActorExterno.EstadoID,
                Clasificaciones = entity.ActorEmpresaClasificaciones
                .Select(x => x.ClasificacionID)
                .ToList()
            };

            var antesJson = JsonSerializer.Serialize(antes);
            var despuesJson = JsonSerializer.Serialize(despues);

            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Actualizar",
                Entidad = "ActorEmpresa",
                EntidadId = id,
                DetalleAntes = antesJson,
                DetalleDespues = despuesJson
            });

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success("Empresa actualizada correctamente", true);
        }

    }
}