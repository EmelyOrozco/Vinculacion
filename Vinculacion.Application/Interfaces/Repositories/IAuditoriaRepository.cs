using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface IAuditoriaRepository
    {
        Task RegistrarAsync(Auditoria auditoria);
        Task<List<Auditoria>> GetAllAsync();
    }
}
