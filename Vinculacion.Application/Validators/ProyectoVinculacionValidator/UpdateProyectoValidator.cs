using FluentValidation;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;

namespace Vinculacion.Application.Validators.ProyectoVinculacionValidator
{
    public class UpdateProyectoValidator : AbstractValidator<UpdateProyectoDto>
    {
        public UpdateProyectoValidator()
        {
            RuleFor(x => x.TituloProyecto)
                .NotEmpty()
                .When(x => x.TituloProyecto != null);

            RuleFor(x => x.DescripcionGeneral)
                .NotEmpty()
                .When(x => x.DescripcionGeneral != null);

            RuleFor(x => x.FechaInicio)
                .LessThan(x => x.FechaFin)
                .When(x => x.FechaInicio.HasValue && x.FechaFin.HasValue);

            RuleFor(x => x.PersonaID)
                .GreaterThan(0)
                .When(x => x.PersonaID.HasValue);

            RuleFor(x => x.RecintoID)
                .GreaterThan(0)
                .When(x => x.RecintoID.HasValue);
        }
    }
}
