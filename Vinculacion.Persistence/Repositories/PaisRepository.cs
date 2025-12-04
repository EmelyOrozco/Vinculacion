using System.Data.Entity;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class PaisRepository : BaseRepository<Pais>, IPaisRepository
    {
        public PaisRepository(VinculacionContext context) : base(context)
        {
        }

        public async Task<bool> PaisExists(decimal? PaisID)
        {
            return await _context.Pais.AnyAsync(e => e.PaisID == PaisID);
        }
    }
}
