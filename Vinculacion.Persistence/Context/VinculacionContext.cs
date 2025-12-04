using Microsoft.EntityFrameworkCore;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Persistence.Context
{
    public class VinculacionContext: DbContext
    {
        public VinculacionContext(DbContextOptions<VinculacionContext> options) : base(options) { }

        public DbSet<TipoPersonaVinculacion> TipoPersonaVinculacion { get; set; }
        public DbSet<ClasificacionEmpresa> ClasificacionEmpresa { get; set; }

        public DbSet<ActorExterno> ActorExterno { get; set; }

        public DbSet<Pais> Pais { get; set; }

        public DbSet<ActorPersona> ActorPersona { get; set; }

        public DbSet<ActorEmpresa> ActorEmpresa { get; set; }
    }
}
