namespace Vinculacion.Application.Dtos.UsuarioSistemaDto
{
    public class UsersDto
    {
        public decimal UsuarioId { get; set; }
        public decimal? Idrol { get; set; }

        public decimal? EstadoId { get; set; }

        public string NombreCompleto { get; set; }

        public string Cedula { get; set; }

        public string Usuario { get; set; }

        public string CorreoInstitucional { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string Password { get; set; }

        public string NombreRol { get; set; }

    }
}
