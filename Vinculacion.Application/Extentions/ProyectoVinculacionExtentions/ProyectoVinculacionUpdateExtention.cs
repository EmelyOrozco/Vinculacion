using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ProyectoVinculacionExtentions
{
    public static class ProyectoVinculacionUpdateExtention
    {
        public static void UpdateFromDto(this ProyectoVinculacion entity, UpdateProyectoDto dto)
        {
            entity.PersonaID = dto.PersonaID;
            entity.RecintoID = dto.RecintoID;
            entity.TituloProyecto = dto.TituloProyecto;
            entity.DescripcionGeneral = dto.DescripcionGeneral;
            entity.FechaInicio = dto.FechaInicio;
            entity.FechaFin = dto.FechaFin;
            entity.Ambito = dto.Ambito;
            entity.Sector = dto.Sector;
            entity.FechaModificacion = DateTime.Now;
        }
    }
}