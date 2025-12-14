using FluentValidation;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;

namespace Vinculacion.Application.Validators.ProyectoVinculacionValidator
{
    public class AddActividadesToProyectoDtoValidator
        : AbstractValidator<AddActividadesToProyectoDto>
    {
        public AddActividadesToProyectoDtoValidator()
        {
            RuleFor(x => x.ActividadesIds)
                .NotEmpty().WithMessage("Debe seleccionar al menos una actividad");
        }
    }
}