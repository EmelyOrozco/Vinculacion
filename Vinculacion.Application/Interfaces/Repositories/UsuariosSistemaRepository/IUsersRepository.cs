using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository
{
    public interface IUsersRepository: IBaseRepository<Usuario>
    {
        Task<Usuario?> GetCredentialsAsync(string codigoEmpleado);
        Task<Usuario?> UsuadioById(decimal id);

        Task<Usuario?> ValidarExistenciaUsuario(string cedula, string codigoEmpleado);

        Task<List<Usuario>> GetCorreoUsuariosAlertas();
    }
}
