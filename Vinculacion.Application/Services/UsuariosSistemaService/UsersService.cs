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

        public async Task<OperationResult<UsersAddDto>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllAsync(l => true);

            if (!users.IsSuccess)
            {
                return OperationResult<UsersAddDto>.Failure("Error obteniendo los usuarios del sistema");
            }
            return OperationResult<UsersAddDto>.Success("Usuarios: ", users.Data);
        }

        public async Task<OperationResult<UsersAddDto>> GetUserById(decimal id)
        {
            var userid = await _usersRepository.GetByIdAsync(id);
            if(userid is null)
            {
                return OperationResult<UsersAddDto>.Failure($"Error obteniendo el usuario con id {id}");
            }
            return OperationResult<UsersAddDto>.Success("Usuario obtenido exitosamente", userid.Data);
        }

        public async Task<OperationResult<UsersUpdateDto>> UpdateUserAsync(UsersUpdateDto usersUpdateDto, decimal id)
        {
            var usuario = await _usersRepository.UsuadioById(id);

            if (usuario is null)
            {
                return OperationResult<UsersUpdateDto>.Failure("Usuario no encontrado");
            }

            var entity = usersUpdateDto.ToUsuarioFromUpdateDto();

            if (usersUpdateDto.Idrol.HasValue && usersUpdateDto.Idrol.Value > 0)
            {
                usuario.Idrol = usersUpdateDto.Idrol.Value;
            }

            if (usersUpdateDto.EstadoId.HasValue && usersUpdateDto.EstadoId.Value > 0)
            {
                usuario.EstadoId = usersUpdateDto.EstadoId.Value;
            }

            if (!string.IsNullOrEmpty(usersUpdateDto.PasswordHash))
            {
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario, usersUpdateDto.PasswordHash);
            }

            usuario.FechaModificacion = DateTime.Now;

            var usuarioeditado = await _usersRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();

            if (!usuarioeditado.IsSuccess)
            {
                return OperationResult<UsersUpdateDto>.Failure("No se pudo actualizar el usuario");
            }
            else
            {
                return OperationResult<UsersUpdateDto>.Success("Usuario Actualizado correctamente", usuarioeditado.Data);
            }                
        }

        private async Task<Usuario?> GetUsuarioOrFail(decimal id)
        {
            var usuario = await _usersRepository.UsuadioById(id);

            if (usuario is null)
            {
                return null;
            }

            return usuario;
        }

        public async Task<OperationResult<bool>> UpdateUserRolAsync(decimal id, UsersUpdateDto usersUpdateDto)
        {
            if (!usersUpdateDto.Idrol.HasValue || usersUpdateDto.Idrol.Value <= 0)
            {
                return OperationResult<bool>.Failure("Rol inválido");
            }

            var usuario = await GetUsuarioOrFail(id);

            if (usuario is null)
            {
                return OperationResult<bool>.Failure("Usuario no encontrado");
            }

            usuario.Idrol = usersUpdateDto.Idrol;
            usuario.FechaModificacion = DateTime.Now;

            var usuarioeditado = await _usersRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();

            if (!usuarioeditado.IsSuccess)
            {
                return OperationResult<bool>.Failure("No se pudo actualizar el usuario");
            }
            else { 

                return OperationResult<bool>.Success("Rol actualizado correctamente", true);
            }
        }

        public async Task<OperationResult<bool>> UpdateUserStateAsync(decimal id, UsersUpdateDto usersUpdateDto)
        {
            if (!usersUpdateDto.EstadoId.HasValue || usersUpdateDto.EstadoId.Value <= 0)
            {
                return OperationResult<bool>.Failure("Estado inválido");
            }

            var usuario = await GetUsuarioOrFail(id);

            if (usuario is null)
            {
                return OperationResult<bool>.Failure("Usuario no encontrado");
            }

            usuario.EstadoId = usersUpdateDto.EstadoId;
            usuario.FechaModificacion = DateTime.Now;

            var estadoeditado = await _usersRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();

            if (!estadoeditado.IsSuccess)
            {
                return OperationResult<bool>.Failure("No se pudo actualizar el usuario");
            }
            else
            {
                return OperationResult<bool>.Success("Usuario Actualizado correctamente", true);
            }  
        }

        public async Task<OperationResult<bool>> UpdateUserPasswordAsync(decimal id, UsersUpdateDto usersUpdateDto)
        {
            if (string.IsNullOrWhiteSpace(usersUpdateDto.PasswordHash))
                return OperationResult<bool>.Failure("La contraseña es requerida");

            var usuario = await GetUsuarioOrFail(id);

            if (usuario is null)
            { 
                return OperationResult<bool>.Failure("Usuario no encontrado");
            }

            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, usersUpdateDto.PasswordHash);

            usuario.FechaModificacion = DateTime.Now;

            var passwordedited = await _usersRepository.Update(usuario);
            await _unitOfWork.SaveChangesAsync();

            if (!passwordedited.IsSuccess)
            {
                return OperationResult<bool>.Failure("No se pudo actualizar el usuario");
            }
            else
            {
                return OperationResult<bool>.Success("Usuario Actualizado correctamente", true);
            }
        }

    }
}
