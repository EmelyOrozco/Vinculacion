using FluentValidation;
using Vinculacion.Application.Features.ActorVinculacion.Commands;
using Vinculacion.Application.Interfaces.Repositories;

namespace Vinculacion.Application.Features.ActorVinculacion.Validators
{
    public class CrearActorPersonaValidator: AbstractValidator<CreateActorPersonaCommand>
    {
        private readonly IPaisRepository _paisRepository;
        public CrearActorPersonaValidator(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;

            RuleFor(x => x.ActorPersona.NombreCompleto)
                .NotEmpty().WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre completo no puede exceder los 200 caracteres.");

            RuleFor(x => x.ActorPersona.Telefono)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(15).WithMessage("El teléfono no puede exceder los 15 caracteres.");

            RuleFor(x => x.ActorPersona.Correo)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El correo no es válido.")
                .MaximumLength(100).WithMessage("El correo no puede exceder los 100 caracteres.");

            RuleFor(x => x.ActorPersona.PaisID)
                .GreaterThan(0).WithMessage("El País es obligatorio.")
                .MustAsync(async (command, paisID, ct) => { return await _paisRepository.PaisExists(paisID); }).WithMessage("El pais seleccionado no existe");
        }

    }
}
