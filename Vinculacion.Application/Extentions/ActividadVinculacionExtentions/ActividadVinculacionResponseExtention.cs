using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class ActividadVinculacionResponseExtention
    {
        public static ActividadVinculacionDto ToActividadVinculacionDto(
    this ActividadVinculacion entity)
        {
            return new ActividadVinculacionDto
            {
                ActorExternoId = entity.ActorExternoId,
                RecintoId = entity.RecintoId,
                TipoVinculacionId = entity.TipoVinculacionId,
                PersonaId = entity.PersonaId,
                EstadoId = entity.EstadoId,
                TituloActividad = entity.TituloActividad,
                DescripcionActividad = entity.DescripcionActividad,
                Modalidad = entity.Modalidad,
                Lugar = entity.Lugar,
                FechaHoraEvento = entity.FechaHoraEvento,
                FechaRegistro = entity.FechaRegistro,
                Ambito = entity.Ambito,
                Sector = entity.Sector,

                Subtareas = entity.Subtareas?
                    .OrderBy(s => s.Orden)
                    .Select(s => new ActividadSubtareasDto
                    {
                        ActividadID = s.ActividadID,
                        EstadoID = s.EstadoID,
                        TituloSubtarea = s.TituloSubtarea,
                        Detalle = s.Detalle,
                        Orden = s.Orden,
                        FechaCompletado = s.FechaCompletado
                    })
                    .ToList()
            };
        }
    }
}