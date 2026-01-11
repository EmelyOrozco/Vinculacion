namespace Vinculacion.Domain.Entities
{
    public class Auditoria
    {
        public decimal AuditoriaID { get; set; }
        public decimal UsuarioID { get; set; }
        public DateTime FechaHora { get; set; }
        public string Accion { get; set; } = null!;
        public string Entidad { get; set; } = null!;
        public decimal? EntidadId { get; set; }
        public string? DetalleAntes { get; set; }
        public string? DetalleDespues { get; set; }
    }
}
