using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService
{
    public interface IUsersService
    {
        Task<OperationResult<UsersAddDto>> ValidateUserAsync(string codigoEmpleado, string password);
        Task<OperationResult<UsersAddDto>> AddUserAsync(UsersAddDto usersDto);
    }
}
