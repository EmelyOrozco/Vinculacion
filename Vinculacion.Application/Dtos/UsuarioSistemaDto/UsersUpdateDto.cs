
namespace Vinculacion.Application.Dtos.UsuarioSistemaDto
{
    public class UsersUpdateDto
    {
        public decimal? Idrol { get; set; }

        public decimal? EstadoId { get; set; }
        public string? PasswordHash { get; set; }
    }
}
