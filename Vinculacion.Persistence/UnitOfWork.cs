using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Persistence.Context;
using Vinculacion.Persistence.Repositories;

namespace Vinculacion.Persistence
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly VinculacionContext _context;
        public IAuditoriaRepository Auditoria { get; }

        public UnitOfWork(VinculacionContext vinculacionContext)
        {
            _context = vinculacionContext;
            Auditoria = new AuditoriaRepository(vinculacionContext);

        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
