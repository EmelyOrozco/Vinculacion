using System.Runtime.CompilerServices;
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

        public async Task<OperationResult<ActividadVinculacionDto>> AddActividadVinculacion(ActividadVinculacionDto actividadVinculacionDto)
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
            var actividad = await _actividadVinculacionRepository.AddAsync(actividadVinculacion);
            var result = await _unitOfWork.SaveChangesAsync();
            
            return OperationResult<ActividadVinculacionDto>.Success("Actividad Vinculacion agregada correctamente ", result);
        }
    }
}
