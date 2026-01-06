using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActividadVinculacionService
{
    public interface IPersonaVinculacionService
    {
        Task<OperationResult<PersonaVinculacionDto>> AddPersonaVinculacion(PersonaVinculacionDto request, decimal usuarioId);
        Task<OperationResult<List<PersonaVinculacionDto>>> GetAllAsync();
        Task<OperationResult<PersonaVinculacionDto>> GetByIdAsync(decimal id);
        Task<OperationResult<bool>> UpdateAsync(decimal id, PersonaVinculacionUpdateDto dto, decimal usuarioId);
    }
}
