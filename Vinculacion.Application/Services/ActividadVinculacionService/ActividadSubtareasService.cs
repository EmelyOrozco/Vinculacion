
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class ActividadSubtareasService: IActividadSubtareasService
    {
        private readonly IActividadSubtareasRepository _actividadSubtareasRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ActividadSubtareasService(IActividadSubtareasRepository actividadSubtareasRepository, IUnitOfWork unitOfWork)
        {
            _actividadSubtareasRepository = actividadSubtareasRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<ActividadSubtareasDto>> AddActividadSubtareas(ActividadSubtareasDto actividadSubtareasDto)
        {
            var actividadSubtareas = actividadSubtareasDto.ToActividadSubtareasFromDto();
            var guardar = await _actividadSubtareasRepository.AddAsync(actividadSubtareas);

            return OperationResult<ActividadSubtareasDto>.Success("", null);
        }
    }
}
