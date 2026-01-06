using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService
{
    public interface IUsersService
    {
        Task<OperationResult<UsersAddDto>> ValidateUserAsync(string codigoEmpleado, string password);
        Task<OperationResult<UsersAddDto>> AddUserAsync(UsersAddDto usersDto, decimal usuarioId);
        Task<OperationResult<UsersAddDto>> GetAllUsersAsync();
        Task<OperationResult<UsersAddDto>> GetUserById(decimal id);
        Task<OperationResult<UsersUpdateDto>> UpdateUserAsync(UsersUpdateDto usersUpdateDto, decimal id, decimal usuarioId);
        Task<OperationResult<bool>> UpdateUserStateAsync(decimal id, UsersUpdateStateDto usersUpdateDto, decimal usuarioId);
        Task<OperationResult<bool>> UpdateUserRolAsync(decimal id, UsersUpdateRolDto usersUpdateDto, decimal usuarioId);
        Task<OperationResult<bool>> UpdateUserPasswordAsync(decimal id, UsersUpdatePassDto usersUpdateDto, decimal usuarioId);
    }
}
