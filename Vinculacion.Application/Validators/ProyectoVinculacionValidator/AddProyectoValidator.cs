using FluentValidation;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;

namespace Vinculacion.Application.Validators.ProyectoVinculacionValidator
{
    public class AddProyectoValidator : AbstractValidator<AddProyectoDto>
    {
        public AddProyectoValidator()
        {
            RuleFor(x => x.ActorExternoID)
                .NotEmpty().WithMessage("El Actor Externo es obligatorio");

            RuleFor(x => x.PersonaID)
                .NotEmpty().WithMessage("Persona vinculante es obligatoria");

            RuleFor(x => x.FechaFin)
                .GreaterThanOrEqualTo(x => x.FechaInicio)
                .When(x => x.FechaInicio.HasValue && x.FechaFin.HasValue)
                .WithMessage("La fecha de fin no puede ser menor que la de inicio");
        }
    }
}
