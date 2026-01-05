using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Persistence.Context
{
    public class VinculacionContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;
        public VinculacionContext(DbContextOptions<VinculacionContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
        }

        public DbSet<TipoPersonaVinculacion> TipoPersonaVinculacion { get; set; }
        public DbSet<ClasificacionEmpresa> ClasificacionEmpresa { get; set; }

        public DbSet<ActorExterno> ActorExterno { get; set; }

        public DbSet<Pais> Pais { get; set; }

        public DbSet<ActorPersona> ActorPersona { get; set; }

        public DbSet<ActorEmpresa> ActorEmpresa { get; set; }

        public DbSet<PersonaVinculacion> PersonaVinculacion { get; set; }

        public DbSet<ActividadVinculacion> ActividadVinculacion { get; set; }

        public DbSet<ActividadSubtareas> ActividadSubtareas { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<ProyectoActividad> ProyectoActividad { get; set; }

        public DbSet<Subida> Subida { get; set; }

        public DbSet<SubidaDetalle> SubidaDetalle { get; set; }
        public DbSet<Recinto> Recintos { get; set; }
        public DbSet<Facultad> Facultades { get; set; }
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<DocumentoAdjunto> DocumentoAdjunto { get; set; }
        public DbSet<Estado> Estado { get; set; }

        public DbSet<Rol> Rol { get; set; }

        public DbSet<ProyectoVinculacion> ProyectoVinculacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorExterno>()
            .HasKey(e => new { e.ActorExternoID });

            modelBuilder.Entity<ActorExterno>().Property(e => e.ActorExternoID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<ActorPersona>()
            .HasKey(e => new { e.ActorExternoID });

            modelBuilder.Entity<ActorPersona>()
            .HasOne(ap => ap.ActorExterno)
            .WithOne()
            .HasForeignKey<ActorPersona>(ap => ap.ActorExternoID);


            modelBuilder.Entity<ActorEmpresa>()
            .HasKey(e => new { e.ActorExternoID });

            modelBuilder.Entity<ActorEmpresa>()
            .HasOne(e => e.ActorExterno)
            .WithOne()
            .HasForeignKey<ActorEmpresa>(e => e.ActorExternoID);


            modelBuilder.Entity<Pais>()
            .HasKey(e => new { e.PaisID });

            modelBuilder.Entity<ActorEmpresaClasificacion>(entity =>
            {
                entity.HasKey(e => new { e.ActorExternoID, e.ClasificacionID });

                entity.HasOne(e => e.ActorEmpresa)
                    .WithMany(a => a.ActorEmpresaClasificaciones)
                    .HasForeignKey(e => e.ActorExternoID);

                entity.HasOne(e => e.ClasificacionEmpresa)
                    .WithMany()
                    .HasForeignKey(e => e.ClasificacionID);
            });


            modelBuilder.Entity<TipoPersonaVinculacion>()
            .HasKey(e => new { e.TipoPersonaID });

            modelBuilder.Entity<ClasificacionEmpresa>()
            .HasKey(e => new { e.ClasificacionID });

            modelBuilder.Entity<PersonaVinculacion>()
            .HasKey(e => new { e.PersonaID });

            modelBuilder.Entity<PersonaVinculacion>().Property(e => e.PersonaID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<ActividadVinculacion>()
                .HasKey(e => new { e.ActividadId });

            modelBuilder.Entity<ActividadVinculacion>().Property(e => e.ActividadId)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<ActividadSubtareas>()
            .HasOne(x => x.ActividadVinculacion)
            .WithMany(x => x.Subtareas)
            .HasForeignKey(x => x.ActividadID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ActividadSubtareas>()
               .HasKey(e => new { e.SubtareaID });

            modelBuilder.Entity<ActividadSubtareas>().Property(e => e.SubtareaID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
               .HasKey(e => new { e.UsuarioId });

            modelBuilder.Entity<Usuario>().Property(e => e.UsuarioId)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.rol)
            .WithMany()
            .HasForeignKey(u => u.Idrol);

            modelBuilder.Entity<Rol>()
               .HasKey(e => new { e.Idrol });

            modelBuilder.Entity<ProyectoVinculacion>()
                .HasKey(e => new { e.ProyectoID });

            modelBuilder.Entity<ProyectoVinculacion>().Property(e => e.ProyectoID)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProyectoActividad>(entity =>
            {
                entity.ToTable("ProyectoActividad");

                entity.HasKey(e => new { e.ProyectoID, e.ActividadID });

                entity.HasOne(e => e.Proyecto)
                    .WithMany(p => p.ProyectoActividades)
                    .HasForeignKey(e => e.ProyectoID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Actividad)
                    .WithMany()
                    .HasForeignKey(e => e.ActividadID)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Auditoria>()
               .HasKey(e =>  e.AuditoriaID );

            modelBuilder.Entity<Subida>().HasKey(e => e.SubidaId);

            modelBuilder.Entity<SubidaDetalle>().HasKey(e => e.SubidaId);
            modelBuilder.Entity<Recinto>().ToTable("Recinto").HasKey(x => x.RecintoID);
            modelBuilder.Entity<Facultad>().ToTable("Facultad").HasKey(x => x.FacultadID);
            modelBuilder.Entity<Escuela>().ToTable("Escuela").HasKey(x => x.EscuelaID);
            modelBuilder.Entity<Carrera>().ToTable("Carrera").HasKey(x => x.CarreraID);

            modelBuilder.Entity<DocumentoAdjunto>().ToTable("DocumentoAdjunto").HasKey(x => x.DocumentoAdjuntoID);
            modelBuilder.Entity<DocumentoAdjunto>().Property(x => x.DocumentoAdjuntoID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("Estado");
                entity.HasKey(e => new { e.EstadoID, e.TablaEstado });

                entity.Property(e => e.Descripcion)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.TablaEstado)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }

    }
}
