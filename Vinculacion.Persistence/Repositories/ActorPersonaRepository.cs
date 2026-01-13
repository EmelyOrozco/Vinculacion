using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActorPersonaRepository : BaseRepository<ActorPersona>, IActorPersonaRepository
    {
        public ActorPersonaRepository(VinculacionContext context) : base(context)
        {
        }

        public async Task<bool> ActorPersonaExists(string Identificacion)
        {
            if (string.IsNullOrWhiteSpace(Identificacion)) return false;
            return await _context.ActorPersona.AnyAsync(e => e.IdentificacionNumero == Identificacion);
        }

        public async Task<ActorPersona?> GetByIdWithActorExternoAsync(decimal id)
        {
            return await _context.ActorPersona
                .Include(p => p.ActorExterno)
                .FirstOrDefaultAsync(p => p.ActorExternoID == id);
        }
        public async Task<List<ActorPersona>> GetAllWithActorExternoAsync()
        {
            return await _context.ActorPersona
                .Include(p => p.ActorExterno)
                .ToListAsync();
        }

    }
}
