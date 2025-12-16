namespace Vinculacion.Domain.Entities
{
    public class Usuario
    {
        public decimal UsuarioId { get; set; }

        public decimal? Idrol { get; set; }

        public decimal? EstadoId { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Cedula { get; set; }

        public string? CodigoEmpleado { get; set; }

        public string? CorreoInstitucional { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? PasswordHash { get; set; }

        public Rol rol { get; set; }
    }
}
