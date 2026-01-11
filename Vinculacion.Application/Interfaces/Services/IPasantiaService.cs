
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Services
{
    public interface IPasantiaService
    {
        Task<decimal> GetPasantiasActivasFinalizadas(decimal pasantiaID);

        Task<List<string>> GetPasantiasActivasFinalizadas();

        Task<OperationResult<List<PasantiaVinculacion>>> GetAllPasantiaVinculacion();
    }
}
