using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Extentions.UsuarioSistemaExtentions;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Vinculacion.Application.Interfaces.Repositories;

namespace Vinculacion.Application.Services.UsuariosSistemaService
{
    public class UsersService: IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        public UsersService(IUsersRepository usersRepository,
            IPasswordHasher<Usuario> passwordHasher,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _unitOfWork = unitOfWork;

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

        public async Task<OperationResult<UsersDto>> AddUserAsync(UsersDto usersDto)
        {
            usersDto.CorreoInstitucional = NormalizarCorreo(usersDto.CorreoInstitucional);

            var correoValido = await _emailService.SendEmail(usersDto.CorreoInstitucional,
                "Validacion de correo",
                "<p>Verificando que este correo exista para completar el registro<p>");

            if (!correoValido)
                return OperationResult<UsersDto>.Failure("El correo institucional no es válido");

            string claveGenerada = GeneratePassword(10);

            var usuarioEntity = usersDto.ToUsuarioFromAddDto();

            usuarioEntity.PasswordHash = _passwordHasher.HashPassword(usuarioEntity, claveGenerada);

            await _usersRepository.AddAsync(usuarioEntity);
            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendEmail(usersDto.CorreoInstitucional,
                "Registro de usuario",
                $"<p>Su usuario ha sido creado exitosamente.</p>" +
                $"<p>Su contraseña temporal es: <strong>{claveGenerada}</strong></p>" +
                $"<p>Por favor, cambie su contraseña después de iniciar sesión.</p>");

            return OperationResult<UsersDto>.Success("Usuario agregado correctamente");
        }

        private string GeneratePassword(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%^&*!";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string NormalizarCorreo(string email)
        {
            if (!email.Contains("@"))
            {
                return email + "@unphu.edu.do";
            }

            var username = email.Split("@")[0];

            return username + "@unphu.edu.do"; 
        }   
    }
}
