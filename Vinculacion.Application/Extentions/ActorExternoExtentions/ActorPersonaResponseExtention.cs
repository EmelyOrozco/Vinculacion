using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActorExternoExtentions
{
    public static class ActorPersonaResponseExtention
    {
        public static ActorPersonaResponseDto ToActorPersonaFromActorPersonaDto(this ActorPersona dto)
        {
            return new ActorPersonaResponseDto
            {
                ActorExternoID = dto.ActorExternoID,
                EstadoID = dto.ActorExterno.EstadoID,
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