using MediatR;
using Vinculacion.Application.Features.ActorVinculacion.Commands;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Features.ActorVinculacion.Handlers
{
    public class CreateActorEmpresaHandler: IRequestHandler<CreateActorEmpresaCommand, int>
    {
        private readonly IActorExternoRepository _actorExternoRepository;
        private readonly IActorEmpresaRepository _actorEmpresaRepository;
        private readonly IActorEmpresaClasificacion _clasificacionEmpresaRepository;

        public CreateActorEmpresaHandler(IActorExternoRepository actorExternoRepository, 
            IActorEmpresaRepository actorEmpresaRepository,
            IActorEmpresaClasificacion clasificacionEmpresaRepository)
        {
            _actorExternoRepository = actorExternoRepository;
            _actorEmpresaRepository = actorEmpresaRepository;
            _clasificacionEmpresaRepository = clasificacionEmpresaRepository;
        }
        public async Task<int> Handle(CreateActorEmpresaCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ActorEmpresa;

            var actorExterno = new ActorExterno(){ TipoActorID = 1};
            await _actorExternoRepository.AddAsync(actorExterno);

            int actorExternoId = (int)actorExterno.ActorExternoID;
            var empresa = new ActorEmpresa ()
            { 
                ActorExternoID = actorExterno.ActorExternoID,
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
            return 1;
        }
    }
}
