using Vinculacion.Application.Features.ActorVinculacion.Commands;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Features.ActorVinculacion.Handlers
{
    public class CreateActorPersonaHandler
    {
        private readonly IActorPersonaRepository _actorPersonaRepository;
        private readonly IActorExternoRepository _actorExternoRepository;
        public CreateActorPersonaHandler(IActorPersonaRepository actorPersonaRepository, IActorExternoRepository actorExternoRepository)
        {
            _actorPersonaRepository = actorPersonaRepository;
            _actorExternoRepository = actorExternoRepository;
        }
        public async Task<int> Handle(CreateActorPersonaCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ActorPersona;
            var ActorExterno = new ActorExterno() { TipoActorID = 2 };
            await _actorExternoRepository.AddAsync(ActorExterno);

            var persona = new ActorPersona()
            {
                ActorExternoID = ActorExterno.ActorExternoID,
                NombreCompleto = dto.NombreCompleto,
                TipoIdentificacion = dto.TipoIdentificacion,
                IdentificacionNumero = dto.IdentificacionNumero,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Sexo = dto.Sexo,
                PaisID = dto.PaisID
            };
            await _actorPersonaRepository.AddAsync(persona);
            return 1;

        }
    }
}

