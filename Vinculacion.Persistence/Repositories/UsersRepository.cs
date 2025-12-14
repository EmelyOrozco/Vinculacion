using Microsoft.EntityFrameworkCore;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class UsersRepository: BaseRepository<Usuario>, IUsersRepository
    {
        public UsersRepository(VinculacionContext context): base(context)
        {
            
        }

        public async Task<Usuario?> GetCredentialsAsync(string codigoEmpleado)
        {
            return await _context.Usuario.Include(u => u.rol).FirstOrDefaultAsync(u => u.CodigoEmpleado == codigoEmpleado && u.EstadoId == 1);
        }

        public async Task<Usuario?> UsuadioById(decimal id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<Usuario?> ValidarExistenciaUsuario(string cedula, string codigoEmpleado)
        {
            return await _context.Usuario.FirstOrDefaultAsync(x => x.Cedula == cedula || x.CodigoEmpleado == codigoEmpleado);
        }

    }
}
