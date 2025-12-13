using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService
{
    public interface IUsersService
    {
        Task<OperationResult<UsersDto>> ValidateUserAsync(string codigoEmpleado, string password);
        Task<OperationResult<UsersDto>> AddUserAsync(UsersDto usersDto);
    }
}
