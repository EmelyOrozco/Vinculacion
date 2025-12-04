using Vinculacion.Application.Features.ActorVinculacion.Dtos;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions
{
    public static class ActorPersonaExtention
    {
        public static ActorPersona ToActorPersonaFromActorPersonaDto(this CreateActorPersonaDto dto)
        {
            return new ActorPersona
            {
                NombreCompleto = dto.NombreCompleto,
                TipoIdentificacion = dto.TipoIdentificacion,
                IdentificacionNumero = dto.IdentificacionNumero,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Sexo = dto.Sexo,
                PaisID = dto.PaisID
            };
        }

    }
}
