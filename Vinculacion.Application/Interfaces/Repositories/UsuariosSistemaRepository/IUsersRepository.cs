using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository
{
    public interface IUsersRepository: IBaseRepository<Usuario>
    {
        Task<Usuario?> GetCredentialsAsync(string codigoEmpleado);
        Task<Usuario?> UsuadioById(decimal id);
    }
}
