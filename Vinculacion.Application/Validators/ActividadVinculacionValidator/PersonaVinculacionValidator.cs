

using FluentValidation;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;

namespace Vinculacion.Application.Validators.ActividadVinculacionValidator
{
    public class PersonaVinculacionValidator: AbstractValidator<PersonaVinculacionDto>
    {
        public PersonaVinculacionValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre completo no puede exceder los 200 caracteres.");

            RuleFor(x => x.TelefonoContacto)
                .NotEmpty().WithMessage("El teléfono de contacto es obligatorio.")
                .MaximumLength(15).WithMessage("El teléfono de contacto no puede exceder los 15 caracteres.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.")
                .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.");

            When(x => x.TipoPersonaID == 1, () =>
            {
                RuleFor(x => x.Matricula)
                    .NotEmpty().WithMessage("La matrícula es obligatoria para personas tipo estudiante.")
                    .MaximumLength(20).WithMessage("La matrícula no puede exceder los 20 caracteres.");

                RuleFor(x => x.CarreraID)
                    .NotNull().WithMessage("La carrera es obligatoria para personas tipo estudiante.");

                RuleFor(x => x.EscuelaID)
                    .NotNull().WithMessage("La carrera es obligatoria para personas tipo estudiante.");

                RuleFor(x => x.RecintoID)
                    .NotNull().WithMessage("El recinto es obligatorio para personas tipo estudiante.");
            });
            When(x => x.TipoPersonaID == 2, () =>
            {
                RuleFor(x => x.CodigoEmpleado)
                    .NotEmpty().WithMessage("El código de empleado es obligatorio para personas tipo empleado.")
                    .MaximumLength(20).WithMessage("El código de empleado no puede exceder los 20 caracteres.");

                RuleFor(x => x.CarreraID)
                   .NotNull().WithMessage("La carrera es obligatoria para personas tipo estudiante.");

                RuleFor(x => x.EscuelaID)
                    .NotNull().WithMessage("La carrera es obligatoria para personas tipo estudiante.");

                RuleFor(x => x.RecintoID)
                    .NotNull().WithMessage("El recinto es obligatorio para personas tipo estudiante.");
            });
            When (x => x.TipoPersonaID == 3, () =>
            {
                RuleFor(x => x.AnoEgreso)
                    .NotNull().WithMessage("El año de egreso es obligatorio para personas tipo egresado.")
                    .GreaterThan(1966).WithMessage("El año de egreso debe ser mayor que 1966.")
                    .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("El año de egreso no puede ser mayor al año actual.");

                RuleFor(x => x.CarreraID)
                  .NotNull().WithMessage("La carrera es obligatoria para personas tipo estudiante.");

                RuleFor(x => x.EscuelaID)
                    .NotNull().WithMessage("La carrera es obligatoria para personas tipo estudiante.");

                RuleFor(x => x.RecintoID)
                    .NotNull().WithMessage("El recinto es obligatorio para personas tipo estudiante.");

            });
            When (x => x.TipoPersonaID == 4, () =>
            {
                RuleFor(x => x.CargoEmpresa)
                    .NotEmpty().WithMessage("El cargo en la empresa es obligatorio para personas tipo empleado de empresa.")
                    .MaximumLength(100).WithMessage("El cargo en la empresa no puede exceder los 100 caracteres.");
            });
        }
    }
}
