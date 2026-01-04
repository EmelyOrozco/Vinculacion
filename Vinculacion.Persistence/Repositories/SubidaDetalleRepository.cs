using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class SubidaDetalleRepository: BaseRepository<SubidaDetalle>, ISubidaDetalleRepository
    {
        public SubidaDetalleRepository(VinculacionContext vinculacion): base(vinculacion)
        {
            
        }

        public async Task<IEnumerable<SubidaDetalle>> GetBySubidaIdAsync(decimal subidaId)
        {
            return await _context.SubidaDetalle.AsNoTracking().Where(x => x.SubidaId == subidaId).ToListAsync();
        }
    }
}
