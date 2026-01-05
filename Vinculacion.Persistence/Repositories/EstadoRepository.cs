using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly VinculacionContext _context;

        public EstadoRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> GetByTablaAsync(string tablaEstado)
        {
            return await _context.Estado
                .Where(e => e.TablaEstado == tablaEstado)
                .OrderBy(e => e.EstadoID)
                .ToListAsync();
        }

        public async Task<List<Estado>> GetAllAsync()
        {
            return await _context.Estado
                .OrderBy(e => e.TablaEstado)
                .ThenBy(e => e.EstadoID)
                .ToListAsync();
        }

    }

}
