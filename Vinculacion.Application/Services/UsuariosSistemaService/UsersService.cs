using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Extentions.UsuarioSistemaExtentions;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Vinculacion.Application.Services.UsuariosSistemaService
{
    public class UsersService: IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        public UsersService(IUsersRepository usersRepository, IPasswordHasher<Usuario> passwordHasher)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<OperationResult<UsersDto>> ValidateUserAsync(string codigoEmpleado, string password)
        {
            var user = await _usersRepository.GetCredentialsAsync(codigoEmpleado);

            if (user is null)
            {
                return OperationResult<UsersDto>.Failure("Credenciales inválidas");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                return OperationResult<UsersDto>.Failure("Credenciales inválidas");
            }
            var userDto = user.ToUsersDtoFromEntity();

            return OperationResult<UsersDto>.Success("Usuario obtenido correctamente", userDto);
        }
    }
}
