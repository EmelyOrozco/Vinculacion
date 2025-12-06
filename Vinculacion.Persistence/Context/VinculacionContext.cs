using Microsoft.EntityFrameworkCore;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Persistence.Context
{
    public class VinculacionContext : DbContext
    {
        public VinculacionContext(DbContextOptions<VinculacionContext> options) : base(options)
        {



        }

        public DbSet<TipoPersonaVinculacion> TipoPersonaVinculacion { get; set; }
        public DbSet<ClasificacionEmpresa> ClasificacionEmpresa { get; set; }

        public DbSet<ActorExterno> ActorExterno { get; set; }

        public DbSet<Pais> Pais { get; set; }

        public DbSet<ActorPersona> ActorPersona { get; set; }

        public DbSet<ActorEmpresa> ActorEmpresa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorExterno>()
            .HasKey(e => new { e.ActorExternoID });

            modelBuilder.Entity<ActorPersona>()
            .HasKey(e => new { e.ActorExternoID });

            modelBuilder.Entity<ActorEmpresa>()
            .HasKey(e => new { e.ActorExternoID });

            modelBuilder.Entity<Pais>()
            .HasKey(e => new { e.PaisID });

            modelBuilder.Entity<ActorEmpresaClasificacion>()
           .HasKey(e => new { e.ActorExternoID, e.ClasificacionID });

            modelBuilder.Entity<TipoPersonaVinculacion>()
         .HasKey(e => new { e.TipoPersonaID });

            modelBuilder.Entity<ClasificacionEmpresa>()
         .HasKey(e => new { e.ClasificacionID });

        }

    }
}
