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
                FechaRegistro = entity.ActorExterno.FechaRegistro,
                NombreEmpresa = entity.NombreEmpresa,
                TipoIdentificacion = entity.TipoIdentificacion,
                IdentificacionNumero = entity.IdentificacionNumero,
                PaisID = entity.PaisID,
                Clasificaciones = entity.ActorEmpresaClasificaciones
                    .Select(c => c.ClasificacionID)
                    .ToList()
            };
        }
    }
}