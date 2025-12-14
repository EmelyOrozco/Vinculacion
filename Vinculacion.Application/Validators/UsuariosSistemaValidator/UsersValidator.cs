using FluentValidation;
using Vinculacion.Application.Dtos.UsuarioSistemaDto;

namespace Vinculacion.Application.Validators.UsuariosSistemaValidator
{
    public class UsersValidator: AbstractValidator<UsersAddDto>
    {
        public UsersValidator()
        {
            RuleFor(x => x.Cedula)
                .NotEmpty().WithMessage("La cédula es obligatoria")
                .Matches(@"^\d{11}$").WithMessage("La cédula debe tener 11 dígitos");

            RuleFor(x => x.CodigoEmpleado)
                .NotEmpty()
                .WithMessage("El codigo de empleado es obligatorio");

            RuleFor(x => x.NombreCompleto)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio");

            RuleFor(x => x.Idrol)
                .NotEmpty()
                .WithMessage("Debe seleccionar un rol para este usuario");
            
        }
    }
}
