using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class ActividadSubtareasQueryExtension
    {
        public static ActividadSubtareasDto ToActividadSubtareasDto(this ActividadSubtareas entity)
        {
            return new ActividadSubtareasDto
            {
                ActividadID = entity.ActividadID,
                EstadoID = entity.EstadoID,
                TituloSubtarea = entity.TituloSubtarea,
                Detalle = entity.Detalle,
                Orden = entity.Orden,
                FechaCompletado = entity.FechaCompletado
            };
        }
    }
}