using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActorExternoExtentions
{
    public static class ActorEmpresaResponseExtension
    {
        public static ActorEmpresaResponseDto ToResponseDto(this ActorEmpresa entity)
        {
            return new ActorEmpresaResponseDto
            {
                ActorExternoID = entity.ActorExternoID,
                EstadoID = entity.ActorExterno.EstadoID,
                NombreEmpresa = entity.NombreEmpresa,
                ContactoNombrePersona = entity.ContactoNombrePersona,
                ContactoCorreo = entity.ContactoCorreo,
                ContactoTelefono = entity.ContactoTelefono,
                PaisID = entity.PaisID,
                Clasificaciones = entity.ActorEmpresaClasificaciones
                    .Select(c => c.ClasificacionID)
                    .ToList()
            };
        }
    }
}