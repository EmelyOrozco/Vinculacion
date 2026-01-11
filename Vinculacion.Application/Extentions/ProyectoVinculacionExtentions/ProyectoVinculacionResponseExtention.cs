using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ProyectoVinculacionExtentions
{
    public static class ProyectoVinculacionResponseExtention
    {
        public static ProyectoResponseDto ToProyectoResponseDto(this ProyectoVinculacion entity)
        {
            return new ProyectoResponseDto
            {
                ProyectoID = entity.ProyectoID,
                ActorExternoID = entity.ActorExternoID,
                PersonaID = entity.PersonaID,
                RecintoID = entity.RecintoID,
                EstadoID = entity.EstadoID,
                TituloProyecto = entity.TituloProyecto,
                DescripcionGeneral = entity.DescripcionGeneral,
                FechaRegistro = entity.FechaRegistro,
                FechaModificacion = entity.FechaModificacion,
                FechaInicio = entity.FechaInicio,
                FechaFin = entity.FechaFin,
                ActividadesIDs = entity.ProyectoActividades
                    .Select(x => x.ActividadID)
                    .ToList()
            };
        }
    }
}
