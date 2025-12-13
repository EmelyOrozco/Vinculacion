using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Extentions.UsuarioSistemaExtentions;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Vinculacion.Application.Interfaces.Repositories;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Vinculacion.Application.Services.UsuariosSistemaService
{
    public class UsersService: IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UsersAddDto> _validator;
        public UsersService(IUsersRepository usersRepository,
            IPasswordHasher<Usuario> passwordHasher,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IValidator<UsersAddDto> validator)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _validator = validator;

        }

        public async Task<OperationResult<UsersAddDto>> ValidateUserAsync(string codigoEmpleado, string password)
        {
            var user = await _usersRepository.GetCredentialsAsync(codigoEmpleado);

            if (user is null)
            {
                return OperationResult<UsersAddDto>.Failure("Credenciales inválidas");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
           
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                return OperationResult<UsersAddDto>.Failure("Credenciales inválidas");
            }

            var userDto = user.ToUsersDtoFromEntity();

            return OperationResult<UsersAddDto>.Success("Usuario obtenido correctamente", userDto);
        }

        public async Task<OperationResult<UsersAddDto>> AddUserAsync(UsersAddDto usersDto)
        {
            var validation = await _validator.ValidateAsync(usersDto);
            if (!validation.IsValid)
            {
                return OperationResult<UsersAddDto>.Failure("Error: ", validation.Errors.Select(x => x.ErrorMessage));
            }

            bool cedulaValidation = ValidateCedula(usersDto.Cedula);

            if (cedulaValidation is false)
            {
                return OperationResult<UsersAddDto>.Failure("La cedula digitada no es valida");
            }

            usersDto.CorreoInstitucional = NormalizarCorreo(usersDto.CorreoInstitucional);

            var correoValido = await _emailService.SendEmail(usersDto.CorreoInstitucional,
                "Validacion de correo",
                "<p>Verificando que este correo exista para completar el registro<p>");

            if (!correoValido)
            { 
                return OperationResult<UsersAddDto>.Failure("El correo institucional no es válido");
            }

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

            return OperationResult<UsersAddDto>.Success("Usuario agregado correctamente");
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
        
        public bool ValidateCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            { 
                return false; 
            }
            cedula = cedula.Replace("-", "").Trim();

            if (!Regex.IsMatch(cedula, @"^\d{11}$"))
            {
                return false;
            }

            if (cedula.Substring(0,3) == "000")
            {
                return false;
            }

            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                int digito = cedula[i] - '0';
                int multiplicador = (i % 2 == 0) ? 1 : 2;

                int resultado = digito * multiplicador;

                if (resultado > 9)
                    resultado = (resultado / 10) + (resultado % 10);

                suma += resultado;
            }

            int digitoVerificador = (10 - (suma % 10)) % 10;

            return digitoVerificador == (cedula[10] - '0');
        }
    }
}
