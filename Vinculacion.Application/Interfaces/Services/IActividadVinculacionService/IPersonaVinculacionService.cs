using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActividadVinculacionService
{
    public interface IPersonaVinculacionService
    {
        Task<OperationResult<PersonaVinculacionDto>> AddPersonaVinculacion(PersonaVinculacionDto request);
    }
}
