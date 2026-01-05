using Vinculacion.Application.Dtos.EstadoDto;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services
{
    public interface IEstadoService
    {
        Task<OperationResult<List<EstadoDto>>> GetEstadosPorTablaAsync(string tablaEstado);
        Task<OperationResult<List<EstadoGeneralDto>>> GetAllAsync();
    }

}