using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class SubidaRepository:  BaseRepository<Subida>, ISubidaRepository
    {
        public SubidaRepository(VinculacionContext vinculacion): base(vinculacion)
        {
            
        }

        public async Task<Subida?> GetSubida(decimal subidaId)
        {
            return await _context.Subida.FirstOrDefaultAsync(x => x.SubidaId == subidaId);
        }
    }
}
