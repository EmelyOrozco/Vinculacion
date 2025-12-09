using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Extentions.UsuarioSistemaExtentions;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.UsuariosSistemaService
{
    public class UsersService: IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<OperationResult<UsersDto>> ValidateUserAsync(string codigoEmpleado, string password)
        {
            var usersResult = await _usersRepository.GetCredentialsAsync(codigoEmpleado, password);

            if (usersResult is null)
            {
                return OperationResult<UsersDto>.Failure("No se pudieron obtener los usuarios");
            }

            var userDto = usersResult.ToUsersDtoFromEntity();

            return OperationResult<UsersDto>.Success("Usuario obtenido correctamente", userDto);
        }
    }
}
