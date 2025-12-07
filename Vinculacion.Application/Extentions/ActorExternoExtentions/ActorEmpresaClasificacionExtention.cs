using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActorExternoExtentions
{
    public static class ActorEmpresaClasificacionExtention
    {
        public static List<ActorEmpresaClasificacion> ToActorEmpresaClasificaciones(
            this AddActorEmpresaDto dto,
            decimal actorExternoID)
        {
            return dto.Clasificaciones.Select(c => new ActorEmpresaClasificacion
            {
                ActorExternoID = actorExternoID,
                ClasificacionID = c
            }).ToList();
        }
    }
}
