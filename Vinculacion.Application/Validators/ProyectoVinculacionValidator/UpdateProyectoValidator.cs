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
                .WithMessage("El título del proyecto es obligatorio");

            RuleFor(x => x.FechaFin)
                .GreaterThanOrEqualTo(x => x.FechaInicio)
                .When(x => x.FechaInicio.HasValue && x.FechaFin.HasValue)
                .WithMessage("La fecha de fin no puede ser menor que la de inicio");
        }
    }
}
