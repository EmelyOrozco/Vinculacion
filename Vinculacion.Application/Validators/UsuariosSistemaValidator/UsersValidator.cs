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
                .Length(11).WithMessage("La cédula debe tener 11 dígitos")
                .Matches(@"^\d{11}$").WithMessage("La cédula solo debe contener números")
                .WithMessage("La cédula debe tener 11 dígitos");

            RuleFor(x => x.CodigoEmpleado)
                .NotEmpty()
                .WithMessage("El codigo de empleado es obligatorio")
                .MaximumLength(10)
                .WithMessage("El codigo empleado no puede exceder los 10 caracteres")
                .NotNull()
                .WithMessage("El codigo empleado debe completarse");

            RuleFor(x => x.NombreCompleto)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio")
                .MaximumLength(150)
                .WithMessage("El nombre completo no puede exceder los 150 caracteres.")
                .NotNull()
                .WithMessage("El nombre completo no puede estar vacio");

            RuleFor(x => x.Idrol)
                .NotEmpty()
                .WithMessage("Debe seleccionar un rol para este usuario")
                .NotNull()
                .WithMessage("Debe asignarle un rol a este usuario")
                .NotEqual(0)
                .WithMessage("Debe asignarle un rol a este usuario");

            RuleFor(x => x.CorreoInstitucional)
                .MaximumLength(50)
                .WithMessage("El correo institucional no puede exceder los 50 caracteres");
            
        }
    }
}
