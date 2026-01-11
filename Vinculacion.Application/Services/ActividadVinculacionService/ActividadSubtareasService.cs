using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class ActividadSubtareasService : IActividadSubtareasService
    {
        private readonly IActividadSubtareasRepository _repo;
        private readonly IActividadVinculacionRepository _actividadRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ActividadSubtareasService(
            IActividadSubtareasRepository repo,
            IActividadVinculacionRepository actividadRepo,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _actividadRepo = actividadRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<List<ActividadSubtareaDto>>> GetByActividadAsync(decimal actividadId)
        {
            var lista = await _repo.GetByActividadIdAsync(actividadId);

            var dto = lista.Select(s => new ActividadSubtareaDto
            {
                SubtareaID = s.SubtareaID,
                ActividadID = s.ActividadID,
                EstadoID = s.EstadoID,
                TituloSubtarea = s.TituloSubtarea,
                Detalle = s.Detalle,
                Orden = s.Orden,
                FechaCompletado = s.FechaCompletado
            }).ToList();

            return OperationResult<List<ActividadSubtareaDto>>
                .Success("Subtareas obtenidas", dto);
        }

        public async Task<OperationResult<ActividadSubtareaDto>> CreateAsync(decimal actividadId, ActividadSubtareaCreateDto dto)
        {
            var actividad = await _actividadRepo.GetByIdAsync(actividadId);
            if (actividad == null)
                return OperationResult<ActividadSubtareaDto>.Failure("Actividad no encontrada");

            var entity = new ActividadSubtareas
            {
                ActividadID = actividadId,
                EstadoID = dto.EstadoID,
                TituloSubtarea = dto.TituloSubtarea,
                Detalle = dto.Detalle,
                Orden = dto.Orden,
                FechaCompletado = dto.FechaCompletado
            };

            await _repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<ActividadSubtareaDto>.Success("Subtarea creada", new ActividadSubtareaDto
            {
                SubtareaID = entity.SubtareaID,
                ActividadID = entity.ActividadID,
                EstadoID = entity.EstadoID,
                TituloSubtarea = entity.TituloSubtarea,
                Detalle = entity.Detalle,
                Orden = entity.Orden,
                FechaCompletado = entity.FechaCompletado
            });
        }

        public async Task<OperationResult<bool>> UpdateAsync(decimal subtareaId,ActividadSubtareaUpdateDto dto)
        {
            var entity = await _repo.GetEntityByIdAsync(subtareaId);

            if (entity == null)
                return OperationResult<bool>.Failure("Subtarea no encontrada");

            if (dto.EstadoID.HasValue)
                entity.EstadoID = dto.EstadoID.Value;

            if (dto.TituloSubtarea != null)
                entity.TituloSubtarea = dto.TituloSubtarea;

            if (dto.Detalle != null)
                entity.Detalle = dto.Detalle;

            if (dto.Orden.HasValue)
                entity.Orden = dto.Orden;

            if (dto.FechaCompletado.HasValue)
                entity.FechaCompletado = dto.FechaCompletado;

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success("Subtarea actualizada", true);
        }

    }

}
