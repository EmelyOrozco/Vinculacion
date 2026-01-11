
namespace Vinculacion.Application.Interfaces.Services
{
    public interface IPasantiaService
    {
        Task<decimal> GetPasantiasActivasFinalizadas(decimal pasantiaID);
    }
}
