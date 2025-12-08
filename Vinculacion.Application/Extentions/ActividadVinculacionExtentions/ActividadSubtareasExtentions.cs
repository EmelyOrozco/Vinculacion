using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class ActividadSubtareasExtentions
    {
        public static ActividadSubtareas ToActividadSubtareasFromDto(this ActividadSubtareasDto actividadSubtareasDto)
        {
            return new ActividadSubtareas
            {
                ActividadID = actividadSubtareasDto.ActividadID,
                EstadoID = actividadSubtareasDto.EstadoID,
                TituloSubtarea = actividadSubtareasDto.TituloSubtarea,
                Detalle = actividadSubtareasDto.Detalle,
                Orden = actividadSubtareasDto.Orden
            };
        }
    }
}
