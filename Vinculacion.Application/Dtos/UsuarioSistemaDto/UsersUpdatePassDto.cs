namespace Vinculacion.Application.Dtos.UsuarioSistemaDto
{
    public class UsersUpdatePassDto
    {
        public string? PasswordHash { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}