using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActorExternoExtentions
{
    public static class ActorEmpresaExtention
    {
        public static ActorEmpresa ToActorEmpresaFromActorEmpresaDto(this AddActorEmpresaDto dto)
        {
            return new ActorEmpresa
            {
                NombreEmpresa = dto.NombreEmpresa,
                TipoIdentificacion = dto.TipoIdentificacion,
                IdentificacionNumero = dto.IdentificacionNumero,
                ContactoNombrePersona = dto.ContactoNombrePersona,
                ContactoCorreo = dto.ContactoCorreo,
                ContactoTelefono = dto.ContactoTelefono,
                ContactoSexoPersona = dto.ContactoSexoPersona,
                PaisID = dto.PaisID
            };
        }
    }
}