using MediatR;
using Vinculacion.Application.Features.ActorVinculacion.Commands;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Features.ActorVinculacion.Handlers
{
    public class CreateActorExternoHandler: IRequestHandler<CreateActorExternoCommand, int>
    {
        private readonly IActorExternoRepository _actorExternoRepository;
        private readonly IActorEmpresaRepository _actorEmpresaRepository;
        private readonly IActorPersonaRepository _actorPersonaRepository;
        private readonly IActorEmpresaClasificacion _clasificacionEmpresaRepository;

        public CreateActorExternoHandler(IActorExternoRepository actorExternoRepository, 
            IActorEmpresaRepository actorEmpresaRepository,
            IActorPersonaRepository actorPersonaRepository, 
            IActorEmpresaClasificacion clasificacionEmpresaRepository)
        {
            _actorExternoRepository = actorExternoRepository;
            _actorEmpresaRepository = actorEmpresaRepository;
            _actorPersonaRepository = actorPersonaRepository;
            _clasificacionEmpresaRepository = clasificacionEmpresaRepository;
        }
        public async Task<int> Handle(CreateActorExternoCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Actor;

            var actorExterno = new ActorExterno()
            {
                TipoActorID = dto.TipoActorID,
                EstatusID = dto.EstatusID
            };
            await _actorExternoRepository.AddAsync(actorExterno);

            int actorExternoId = (int)actorExterno.ActorExternoID;

            if (dto.TipoActorID == 1)
            {
                var empresa = new ActorEmpresa ()
                { 
                    ActorExternoID = actorExternoId,
                    NombreEmpresa = dto.NombreEmpresa,
                    TipoIdentificacion = dto.TipoIdentificacion,
                    IdentificacionNumero = dto.IdentificacionNumero,
                    ContactoNombrePersona = dto.ContactoNombrePersona,
                    ContactoCorreo = dto.ContactoCorreo,
                    ContactoTelefono = dto.ContactoTelefono,
                    ContactoSexoPersona = dto.ContactoSexoPersona,
                    PaisID = dto.PaisID
                };
                 await _actorEmpresaRepository.AddAsync(empresa);

                if (dto.Clasificaciones is not null)
                {
                    foreach (var clasificacionID in dto.Clasificaciones)
                    {
                        var clasificacion = new ActorEmpresaClasificacion()
                        {
                           ActorExternoID = actorExternoId,
                            ClasificacionID = clasificacionID
                        };

                        await _clasificacionEmpresaRepository.AddAsync(clasificacion);

                    }
                }
            }

            else if (dto.TipoActorID == 2)
            {
                var persona = new ActorPersona()
                {
                    ActorExternoID = actorExternoId,
                    NombreCompleto = dto.NombreCompleto,
                    TipoIdentificacion = dto.TipoIdentificacion,
                    IdentificacionNumero = dto.IdentificacionNumero,
                    Correo = dto.Correo,
                    Telefono = dto.Telefono,
                    Sexo = dto.Sexo,
                    PaisID = dto.PaisID
                };
                await _actorPersonaRepository.AddAsync(persona);
            }

            
            return 1;
        }
    }
}
