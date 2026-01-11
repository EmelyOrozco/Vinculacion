
namespace Vinculacion.Application.Interfaces.Services
{
    public interface ICharlaService
    {
        Task<decimal> GetCharlasActivasFinalizadas(decimal charlaId);
    }
}
