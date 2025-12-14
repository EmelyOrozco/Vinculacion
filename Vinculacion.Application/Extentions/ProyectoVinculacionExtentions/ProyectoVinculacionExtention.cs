using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ProyectoVinculacionExtentions
{
    public static class ProyectoVinculacionExtention
    {
        public static ProyectoVinculacion ToProyectoFromAddDto(this AddProyectoDto dto)
        {
            return new ProyectoVinculacion
            {
                ActorExternoID = dto.ActorExternoID,
                PersonaID = dto.PersonaID,
                RecintoID = dto.RecintoID,
                TituloProyecto = dto.TituloProyecto,
                DescripcionGeneral = dto.DescripcionGeneral,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Ambito = dto.Ambito,
                Sector = dto.Sector,
                EstadoID = 4,
                FechaRegistro = DateTime.Now
            };
        }
    }
}