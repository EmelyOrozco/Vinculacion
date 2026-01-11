namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IAuditoriaRepository Auditoria { get; }
    }
}
