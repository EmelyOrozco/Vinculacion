using FluentValidation;
using Vinculacion.Application.Dtos.ActorExternoDtos;

public class AddActorEmpresaDtoValidator: AbstractValidator<AddActorEmpresaDto>
{
    public AddActorEmpresaDtoValidator()
    {
        RuleFor(x => x.NombreEmpresa)
            .NotEmpty().WithMessage("El nombre de la empresa es obligatorio")
            .NotNull().WithMessage("El nombre de la empresa es obligatorio")
            .NotEqual("string").WithMessage("El nombre de la empresa es obligatorio")
            .MaximumLength(100).WithMessage("El nombre de la empresa no puede exceder los 100 caracteres");

        RuleFor(x => x.PaisID)
            .NotNull().WithMessage("El país es obligatorio")
            .NotEqual(0).WithMessage("El país es obligatorio")
            .NotEmpty().WithMessage("El país es obligatorio");

        RuleFor(x => x.ContactoNombrePersona)
            .NotEmpty().WithMessage("El nombre de contacto es obligatorio")
            .NotNull().WithMessage("El nombre de contacto es obligatorio")
            .NotEqual("string").WithMessage("El nombre de contacto es obligatorio")
            .MaximumLength(150).WithMessage("El nombre de contacto no puede exceder los 150 caracteres");

        RuleFor(x => x.ContactoTelefono)
               .NotEmpty()
               .WithMessage("El teléfono es obligatorio.")
               .MaximumLength(10)
               .WithMessage("El teléfono no puede exceder los 10 caracteres.")
               .NotEmpty()
               .WithMessage("El teléfono es obligatorio.");

        RuleFor(x => x.ContactoCorreo)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no es válido.")
            .MaximumLength(50).WithMessage("El correo no puede exceder los 100 caracteres.")
            .NotNull()
            .WithMessage("El correo es obligatorio.")
            .NotEqual("string");

        RuleFor(x => x.ContactoSexoPersona)
             .NotEmpty()
             .WithMessage("El sexo de la persona es obligatorio")
             .NotEmpty().WithMessage("El sexo de la persona es obligatorio")
             .NotEqual(0).WithMessage("El sexo de la persona es obligatorio");

        RuleFor(x => x.Clasificaciones)
            .NotEmpty().WithMessage("Debe seleccionar al menos una clasificación")
            .NotNull().WithMessage("Debe seleccionar al menos una clasificación");

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