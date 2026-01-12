using FluentValidation;
using System.Text.Json;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class PersonaVinculacionService: IPersonaVinculacionService
    {
        private readonly IPersonaVinculacionRepository _personaVinculacionRepository;
        private readonly IValidator<PersonaVinculacionDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFacultadRepository _facultadRepository;
        private readonly IEscuelaRepository _escuelaRepository;
        private readonly ICarreraRepository _carreraRepository;

        public PersonaVinculacionService(
            IPersonaVinculacionRepository personaVinculacionRepository,
            IValidator<PersonaVinculacionDto> validator,
            IUnitOfWork unitOfWork,
            IFacultadRepository facultadRepository,
            IEscuelaRepository escuelaRepository,
            ICarreraRepository carreraRepository)
        {
            _personaVinculacionRepository = personaVinculacionRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _facultadRepository = facultadRepository;
            _escuelaRepository = escuelaRepository;
            _carreraRepository = carreraRepository;
        }

        public async Task<OperationResult<PersonaVinculacionDto>> AddPersonaVinculacion(PersonaVinculacionDto personaVinculacionDto, decimal usuarioId)
        {
            var personaVinculacion = personaVinculacionDto.ToPersonaVinculacionFromDto();

            var result = await _validator.ValidateAsync(personaVinculacionDto);

            if (!result.IsValid)
            {
                return OperationResult<PersonaVinculacionDto>.Failure("Error: ", result.Errors.Select(x => x.ErrorMessage));
            }

            if (personaVinculacionDto.FacultadID.HasValue)
            {
                var facultad = await _facultadRepository.GetByIdAsync(personaVinculacionDto.FacultadID.Value);
                if (facultad == null)
                    return OperationResult<PersonaVinculacionDto>
                        .Failure("La facultad seleccionada no existe");
            }

            if (personaVinculacionDto.EscuelaID.HasValue)
            {
                var escuela = await _escuelaRepository.GetByIdAsync(personaVinculacionDto.EscuelaID.Value);

                if (escuela == null)
                    return OperationResult<PersonaVinculacionDto>
                        .Failure("La escuela seleccionada no existe");

                if (personaVinculacionDto.FacultadID.HasValue &&
                    escuela.FacultadID != personaVinculacionDto.FacultadID.Value)
                {
                    return OperationResult<PersonaVinculacionDto>
                        .Failure("La escuela no pertenece a la facultad seleccionada");
                }
            }

            if (personaVinculacionDto.CarreraID.HasValue)
            {
                var carrera = await _carreraRepository.GetByIdAsync(personaVinculacionDto.CarreraID.Value);

                if (carrera == null)
                    return OperationResult<PersonaVinculacionDto>
                        .Failure("La carrera seleccionada no existe");

                if (personaVinculacionDto.EscuelaID.HasValue &&
                    carrera.EscuelaID != personaVinculacionDto.EscuelaID.Value)
                {
                    return OperationResult<PersonaVinculacionDto>
                        .Failure("La carrera no pertenece a la escuela seleccionada");
                }
            }


            var guardar = await _personaVinculacionRepository.AddAsync(personaVinculacion);
            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Crear",
                Entidad = "PersonaVinculacion",
                EntidadId = null
            });
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<PersonaVinculacionDto>.Success("Persona vincunlante guardada correctamente", personaVinculacionDto);
        }

        public async Task<OperationResult<List<PersonaVinculacionDto>>> GetAllAsync()
        {
            var result = await _personaVinculacionRepository.GetAllAsync(x => true);

            if (!result.IsSuccess)
                return OperationResult<List<PersonaVinculacionDto>>.Failure("Error obteniendo personas vinculadas");

            var entidades = (List<PersonaVinculacion>)result.Data;

            var data = entidades
                .Select(x => x.ToPersonaVinculacionDto())
                .ToList();


            return OperationResult<List<PersonaVinculacionDto>>.Success("Personas vinculadas", data);
        }

        public async Task<OperationResult<PersonaVinculacionDto>> GetByIdAsync(decimal id)
        {
            var result = await _personaVinculacionRepository.GetByIdAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<PersonaVinculacionDto>.Failure("Persona vinculada no encontrada");

            PersonaVinculacion entity = (PersonaVinculacion)result.Data;

            return OperationResult<PersonaVinculacionDto>.Success(
                "Persona vinculada encontrada",
                entity.ToPersonaVinculacionDto()
            );
        }


        public async Task<OperationResult<bool>> UpdateAsync(decimal id, PersonaVinculacionUpdateDto dto, decimal usuarioId)
        {
            var entityResult = await _personaVinculacionRepository.GetByIdAsync(id);

            if (!entityResult.IsSuccess || entityResult.Data == null)
                return OperationResult<bool>.Failure("Persona vinculada no encontrada");

            var entity = entityResult.Data;

            decimal? facultadIdFinal = dto.FacultadID ?? entity.FacultadID;
            decimal? escuelaIdFinal = dto.EscuelaID ?? entity.EscuelaID;
            decimal? carreraIdFinal = dto.CarreraID ?? entity.CarreraID;

            if (facultadIdFinal.HasValue)
            {
                var facultad = await _facultadRepository.GetByIdAsync(facultadIdFinal.Value);
                if (facultad == null)
                    return OperationResult<bool>.Failure("La facultad seleccionada no existe");
            }

            if (escuelaIdFinal.HasValue)
            {
                var escuela = await _escuelaRepository.GetByIdAsync(escuelaIdFinal.Value);
                if (escuela == null)
                    return OperationResult<bool>.Failure("La escuela seleccionada no existe");

                if (facultadIdFinal.HasValue &&
                    escuela.FacultadID != facultadIdFinal.Value)
                {
                    return OperationResult<bool>.Failure(
                        "La escuela no pertenece a la facultad seleccionada. Debe cambiar la escuela y la carrera."
                    );
                }
            }

            if (carreraIdFinal.HasValue)
            {
                var carrera = await _carreraRepository.GetByIdAsync(carreraIdFinal.Value);
                if (carrera == null)
                    return OperationResult<bool>.Failure("La carrera seleccionada no existe");

                if (escuelaIdFinal.HasValue &&
                    carrera.EscuelaID != escuelaIdFinal.Value)
                {
                    return OperationResult<bool>.Failure(
                        "La carrera no pertenece a la escuela seleccionada. Debe cambiar la carrera."
                    );
                }
            }

            var antes = JsonSerializer.Serialize(new
            {
                entity.TipoPersonaID,
                entity.RecintoID,
                entity.FacultadID,
                entity.EscuelaID,
                entity.CarreraID,
                entity.NombreCompleto,
                entity.Correo,
                entity.TelefonoContacto,
                entity.TipoRelacion,
                entity.Matricula,
                entity.CodigoEmpleado,
                entity.AnoEgreso,
                entity.CargoEmpresa
            });


            if (dto.TipoPersonaID.HasValue && dto.TipoPersonaID > 0)
                entity.TipoPersonaID = dto.TipoPersonaID.Value;

            if (dto.RecintoID.HasValue && dto.RecintoID > 0)
                entity.RecintoID = dto.RecintoID.Value;

            if (dto.FacultadID.HasValue && dto.FacultadID > 0)
                entity.FacultadID = dto.FacultadID.Value;

            if (dto.EscuelaID.HasValue && dto.EscuelaID > 0)
                entity.EscuelaID = dto.EscuelaID.Value;

            if (dto.CarreraID.HasValue && dto.CarreraID > 0)
                entity.CarreraID = dto.CarreraID.Value;

            if (!string.IsNullOrWhiteSpace(dto.NombreCompleto))
                entity.NombreCompleto = dto.NombreCompleto;

            if (!string.IsNullOrWhiteSpace(dto.Correo))
                entity.Correo = dto.Correo;

            if (!string.IsNullOrWhiteSpace(dto.TelefonoContacto))
                entity.TelefonoContacto = dto.TelefonoContacto;

            if (!string.IsNullOrWhiteSpace(dto.TipoRelacion))
                entity.TipoRelacion = dto.TipoRelacion;

            if (!string.IsNullOrWhiteSpace(dto.Matricula))
                entity.Matricula = dto.Matricula;

            if (!string.IsNullOrWhiteSpace(dto.CodigoEmpleado))
                entity.CodigoEmpleado = dto.CodigoEmpleado;

            if (dto.AnoEgreso.HasValue && dto.AnoEgreso > 0)
                entity.AnoEgreso = dto.AnoEgreso.Value;

            if (!string.IsNullOrWhiteSpace(dto.CargoEmpresa))
                entity.CargoEmpresa = dto.CargoEmpresa;

            var despues = JsonSerializer.Serialize(new
            {
                entity.TipoPersonaID,
                entity.RecintoID,
                entity.FacultadID,
                entity.EscuelaID,
                entity.CarreraID,
                entity.NombreCompleto,
                entity.Correo,
                entity.TelefonoContacto,
                entity.TipoRelacion,
                entity.Matricula,
                entity.CodigoEmpleado,
                entity.AnoEgreso,
                entity.CargoEmpresa
            });


            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Actualizar",
                Entidad = "PersonaVinculacion",
                EntidadId = id,
                DetalleAntes = antes,
                DetalleDespues = despues
            });

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success("Persona vinculada actualizada correctamente", true);
        }
    }
}
