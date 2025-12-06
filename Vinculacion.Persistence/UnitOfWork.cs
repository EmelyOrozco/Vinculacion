using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly VinculacionContext _context;

        public UnitOfWork(VinculacionContext vinculacionContext)
        {
            _context = vinculacionContext;
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
