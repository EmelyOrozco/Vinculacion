
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Services
{
    public interface ICharlaService
    {
        Task<decimal> GetCharlasActivasFinalizadas(decimal charlaId);

        Task<List<string>> GetCharlasActivasFinalizadas();

        Task<OperationResult<List<CharlaVinculacion>>> GetAllCharlaVinculacion();
    }
}
