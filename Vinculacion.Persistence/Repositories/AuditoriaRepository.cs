using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly VinculacionContext _context;

        public AuditoriaRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task RegistrarAsync(Auditoria auditoria)
        {
            await _context.Auditoria.AddAsync(auditoria);
        }

        public async Task<List<Auditoria>> GetAllAsync()
        {
            return await _context.Auditoria
                .OrderByDescending(x => x.FechaHora)
                .ToListAsync();
        }
    }

}
