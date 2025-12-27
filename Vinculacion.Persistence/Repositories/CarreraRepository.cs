using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly VinculacionContext _context;

        public CarreraRepository(VinculacionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Carrera>> GetByEscuelaAsync(decimal escuelaId)
        {
            return await _context.Carreras
                .AsNoTracking()
                .Where(x => x.EscuelaID == escuelaId)
                .OrderBy(x => x.Descripcion)
                .ToListAsync();
        }
    }
}
