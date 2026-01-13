using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class ActividadVinculacionExtention
    {
        public static ActividadVinculacion ToActividadVinculacionFromDto(this ActividadVinculacionDto actividadVinculacionDto)
        {
            return new ActividadVinculacion
            {
                ActorExternoId = actividadVinculacionDto.ActorExternoId,
                RecintoId = actividadVinculacionDto.RecintoId,
                TipoVinculacionId = actividadVinculacionDto.TipoVinculacionId,
                PersonaId = actividadVinculacionDto.PersonaId,
                TituloActividad = actividadVinculacionDto.TituloActividad,
                DescripcionActividad = actividadVinculacionDto.DescripcionActividad,
                Modalidad = actividadVinculacionDto.Modalidad,
                Lugar = actividadVinculacionDto.Lugar,
                FechaHoraEvento = actividadVinculacionDto.FechaHoraEvento,
                FechaRegistro = actividadVinculacionDto.FechaRegistro
            };
        }
    }
}
