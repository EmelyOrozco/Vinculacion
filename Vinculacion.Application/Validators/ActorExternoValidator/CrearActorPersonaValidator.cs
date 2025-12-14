using FluentValidation;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;

namespace Vinculacion.Application.Validators.ActorExternoValidator
{
    public class CrearActorPersonaValidator: AbstractValidator<AddActorPersonaDto>
    {
        public CrearActorPersonaValidator(IPaisRepository paisRepository)
        {

            RuleFor(x => x.NombreCompleto)
                .NotEmpty()
                .WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(150)
                .WithMessage("El nombre completo no puede exceder los 150 caracteres.")
                .NotNull()
                .WithMessage("El nombre completo es obligatorio.")
                .NotEqual("string");

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage("El teléfono es obligatorio.")
                .MaximumLength(10)
                .WithMessage("El teléfono no puede exceder los 10 caracteres.")
                .NotEmpty()
                .WithMessage("El teléfono es obligatorio.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.")
                .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.")
                .NotNull()
                .WithMessage("El correo es obligatorio.")
                .NotEqual("string");

            When(x => x.TipoIdentificacion == 1, () =>
            {
                RuleFor(x => x.IdentificacionNumero)
                    .NotEmpty()
                    .WithMessage("El no. de identificacion debe completarse")
                    .Length(11).WithMessage("La cédula debe tener 11 dígitos")
                    .Matches(@"^\d{11}$").WithMessage("La cédula solo debe contener números")
                    .NotNull()
                    .WithMessage("El no. de identificacion debe completarse")
                    .NotEqual("string");

            });

            When(x => x.TipoIdentificacion == 2, () =>
            {
                RuleFor(x => x.IdentificacionNumero)
                    .NotEmpty()
                    .WithMessage("El no. de identificacion debe completarse")
                    .Length(6, 9).WithMessage("El pasaporte debe tener entre 6 y 9 caracteres")
                    .Matches(@"^[A-Z0-9]+$").WithMessage("El pasaporte solo puede contener letras mayúsculas y números")
                    .NotNull()
                    .WithMessage("El no. de identificacion debe completarse")
                    .NotEqual("string");

            });

            When(x => x.TipoIdentificacion == 3, () =>
            {
                RuleFor(x => x.IdentificacionNumero)
                    .NotEmpty()
                    .WithMessage("El no. de identificacion debe completarse")
                    .Length(9).WithMessage("El RNC debe tener exactamente 9 dígitos")
                    .Matches(@"^\d{9}$").WithMessage("El RNC solo debe contener números")
                    .NotNull()
                    .WithMessage("El no. de identificacion debe completarse")
                    .NotEqual("string");

            });

        }
    }
}
