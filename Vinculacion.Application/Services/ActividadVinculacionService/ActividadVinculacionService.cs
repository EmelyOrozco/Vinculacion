using System.Text.Json;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class ActividadVinculacionService: IActividadVinculacionService
    {
        private readonly IActividadVinculacionRepository _actividadVinculacionRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public ActividadVinculacionService(IActividadVinculacionRepository actividadVinculacionRepository, IUnitOfWork unitOfWork)
        {
            _actividadVinculacionRepository = actividadVinculacionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<ActividadVinculacionDto>> AddActividadVinculacion(ActividadVinculacionDto actividadVinculacionDto, decimal usuarioId)
        {
            var actividadVinculacion = actividadVinculacionDto.ToActividadVinculacionFromDto();

            if (actividadVinculacionDto.Subtareas is not null)
            {
                actividadVinculacion.Subtareas = new List<ActividadSubtareas>();
                foreach (var subtareaDto in actividadVinculacionDto.Subtareas)
                {
                    actividadVinculacion.Subtareas.Add(subtareaDto.ToActividadSubtareasFromDto());

                }
            }
            await _actividadVinculacionRepository.AddAsync(actividadVinculacion);
            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Crear",
                Entidad = "ActividadVinculacion",
                EntidadId = null
            });
            var result = await _unitOfWork.SaveChangesAsync();
            
            return OperationResult<ActividadVinculacionDto>.Success("Actividad Vinculacion agregada correctamente ", result);
        }

        public async Task<OperationResult<List<ActividadVinculacionDto>>> GetAllAsync()
        {
            var result = await _actividadVinculacionRepository.GetAllWithSubtareasAsync();

            if (!result.IsSuccess)
                return OperationResult<List<ActividadVinculacionDto>>
                    .Failure("Error obteniendo actividades");

            var entidades = (List<ActividadVinculacion>)result.Data;

            var data = entidades
                .Select(x => x.ToActividadVinculacionDto())
                .ToList();

            return OperationResult<List<ActividadVinculacionDto>>
                .Success("Actividades obtenidas", data);
        }

        public async Task<OperationResult<ActividadVinculacionDto>> GetByIdAsync(decimal id)
        {
            var result = await _actividadVinculacionRepository.GetByIdWithSubtareasAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<ActividadVinculacionDto>
                    .Failure("Actividad no encontrada");

            var entidad = (ActividadVinculacion)result.Data;

            var dto = entidad.ToActividadVinculacionDto();

            return OperationResult<ActividadVinculacionDto>
                .Success("Actividad encontrada", dto);
        }


        public async Task<OperationResult<bool>> UpdateAsync(decimal id, ActividadVinculacionDto dto, decimal usuarioId)
        {
            var result = await _actividadVinculacionRepository.GetByIdWithSubtareasAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<bool>.Failure("Actividad no encontrada");

            var entity = result.Data as ActividadVinculacion;
            if (entity == null)
                return OperationResult<bool>.Failure("Actividad no encontrada");

            var subtareasSnapshot = entity.Subtareas
            .Select(s => new
            {
                s.SubtareaID,
                s.TituloSubtarea,
                s.Detalle,
                s.Orden,
                s.EstadoID,
                s.FechaCompletado
            })
            .ToList();

            var antes = JsonSerializer.Serialize(new
            {
                entity.ActorExternoId,
                entity.RecintoId,
                entity.TipoVinculacionId,
                entity.PersonaId,
                entity.EstadoId,
                entity.TituloActividad,
                entity.Modalidad,
                entity.Lugar,
                entity.FechaHoraEvento,
                entity.Ambito,
                entity.Sector,
                Subtareas = subtareasSnapshot
            });


            if (dto.ActorExternoId.HasValue && dto.ActorExternoId > 0)
                entity.ActorExternoId = dto.ActorExternoId.Value;

            if (dto.RecintoId.HasValue && dto.RecintoId > 0)
                entity.RecintoId = dto.RecintoId.Value;

            if (dto.TipoVinculacionId.HasValue && dto.TipoVinculacionId > 0)
                entity.TipoVinculacionId = dto.TipoVinculacionId.Value;

            if (dto.PersonaId.HasValue && dto.PersonaId > 0)
                entity.PersonaId = dto.PersonaId.Value;

            if (dto.EstadoId.HasValue && dto.EstadoId > 0)
                entity.EstadoId = dto.EstadoId.Value;

            if (!string.IsNullOrWhiteSpace(dto.TituloActividad))
                entity.TituloActividad = dto.TituloActividad;

            if (!string.IsNullOrWhiteSpace(dto.DescripcionActividad))
                entity.DescripcionActividad = dto.DescripcionActividad;

            if (dto.Modalidad.HasValue && dto.Modalidad > 0)
                entity.Modalidad = dto.Modalidad.Value;

            if (!string.IsNullOrWhiteSpace(dto.Lugar))
                entity.Lugar = dto.Lugar;

            if (dto.FechaHoraEvento.HasValue)
                entity.FechaHoraEvento = dto.FechaHoraEvento;

            if (dto.Ambito.HasValue && dto.Ambito > 0)
                entity.Ambito = dto.Ambito.Value;

            if (dto.Sector.HasValue && dto.Sector > 0)
                entity.Sector = dto.Sector.Value;

            if (dto.Subtareas != null)
            {
                entity.Subtareas.Clear();

                foreach (var subtareaDto in dto.Subtareas)
                {
                    entity.Subtareas.Add(subtareaDto.ToActividadSubtareasFromDto());
                }
            }

            var subtareasDespues = entity.Subtareas
            .Select(s => new
            {
                s.SubtareaID,
                s.TituloSubtarea,
                s.Detalle,
                s.Orden,
                s.EstadoID,
                s.FechaCompletado
            })
            .ToList();

            var despues = JsonSerializer.Serialize(new
            {
                entity.ActorExternoId,
                entity.RecintoId,
                entity.TipoVinculacionId,
                entity.PersonaId,
                entity.EstadoId,
                entity.TituloActividad,
                entity.Modalidad,
                entity.Lugar,
                entity.FechaHoraEvento,
                entity.Ambito,
                entity.Sector,
                Subtareas = subtareasDespues
            });

            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Actualizar",
                Entidad = "ActividadVinculacion",
                EntidadId = id,
                DetalleAntes = antes,
                DetalleDespues = despues
            });

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success("Actividad actualizada correctamente", true);
        }
    }
}
