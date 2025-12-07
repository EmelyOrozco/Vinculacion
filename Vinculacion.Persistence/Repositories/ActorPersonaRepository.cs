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
            return await _context.ActorPersona.AnyAsync(e => e.IdentificacionNumero == Identificacion);
        }

       
    }
}
