using FluentValidation;
using Vinculacion.Application.Dtos.ActorExternoDtos;

public class AddActorEmpresaDtoValidator
    : AbstractValidator<AddActorEmpresaDto>
{
    public AddActorEmpresaDtoValidator()
    {
        RuleFor(x => x.NombreEmpresa)
            .NotEmpty().WithMessage("El nombre de la empresa es obligatorio");

        RuleFor(x => x.PaisID)
            .NotNull().WithMessage("El país es obligatorio");
    }
}
