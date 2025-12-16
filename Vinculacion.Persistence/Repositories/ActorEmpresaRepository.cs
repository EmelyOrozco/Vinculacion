using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActorEmpresaRepository : BaseRepository<ActorEmpresa>, IActorEmpresaRepository
    {
        public ActorEmpresaRepository(VinculacionContext context) : base(context)
        {
        }
        public async Task<List<ActorEmpresa>> GetAllWithClasificacionesAsync()
        {
            return await _context.ActorEmpresa
                .Include(e => e.ActorExterno)
                .Include(e => e.ActorEmpresaClasificaciones)
                .ToListAsync();
        }

        public async Task<ActorEmpresa?> GetByIdWithClasificacionesAsync(decimal id)
        {
            return await _context.ActorEmpresa
                .Include(e => e.ActorExterno)
                .Include(e => e.ActorEmpresaClasificaciones)
                .FirstOrDefaultAsync(e => e.ActorExternoID == id);
        }

        public async Task<ActorEmpresa?> ActorEmpresaExistsAsync (string identificacionNumero, string NombreEmpresa)
        {
            return await _context.ActorEmpresa.FirstOrDefaultAsync(x => x.IdentificacionNumero == identificacionNumero);
        }

    }
}