using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class ActividadVinculacionRepository: BaseRepository<ActividadVinculacion>, IActividadVinculacionRepository
    {
        public ActividadVinculacionRepository(VinculacionContext context): base(context)
        {
        }

        public async Task<List<ActividadVinculacion>>GetActividadesByProyectoId(decimal proyectoId)
        {
            return await _context.ProyectoActividad
                .Where(pa => pa.ProyectoID == proyectoId)
                .Include(pa => pa.Actividad)
                    .ThenInclude(a => a.Subtareas)
                .Select(pa => pa.Actividad)
                .ToListAsync();
        }
        public async Task<List<ActividadVinculacion>> GetActividadesDisponibles()
        {
            return await _context.ActividadVinculacion
                .Include(a => a.Subtareas)
                .Where(a => !_context.ProyectoActividad
                    .Any(pa => pa.ActividadID == a.ActividadId))
                .ToListAsync();
        }

        public async Task<OperationResult<List<ActividadVinculacion>>> GetAllWithSubtareasAsync()
        {
            var data = await _dbSet
                .Include(x => x.Subtareas)
                .ToListAsync();

            return OperationResult<List<ActividadVinculacion>>
                .Success("OK", data);
        }

        public async Task<OperationResult<ActividadVinculacion>> GetByIdWithSubtareasAsync(decimal id)
        {
            var data = await _dbSet
                .Include(x => x.Subtareas)
                .FirstOrDefaultAsync(x => x.ActividadId == id);

            if (data == null)
                return OperationResult<ActividadVinculacion>.Failure("No encontrada");

            return OperationResult<ActividadVinculacion>.Success("OK", data);
        }

        public async Task<List<ActividadVinculacion>> GetActividadEstatusActivo()
        {
            var actividadesActivas = await _context.ActividadVinculacion.Join(_context.Estado, p => p.EstadoId, e => e.EstadoID, (p, e) => new { Actividad = p, Estado = e })
             .Where(x =>
                     x.Estado.TablaEstado == "ActividadVinculacion" &&
                     x.Estado.Descripcion == "Activo")
             .Select(x => x.Actividad)
             .ToListAsync();

            return actividadesActivas;
        }

        public async Task<List<ActividadVinculacion>>GetActividadesDisponiblesByActorExterno(decimal actorExternoId)
        {
            return await _context.ActividadVinculacion
                .Where(a =>
                    a.ActorExternoId == actorExternoId &&
                    !_context.ProyectoActividad.Any(pa => pa.ActividadID == a.ActividadId)
                )
                .ToListAsync();
        }
    }
}
