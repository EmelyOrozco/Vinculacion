using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ProyectoVinculacionExtentions
{
    public static class ProyectoVinculacionUpdateExtention
    {
        public static void UpdateFromDto(this ProyectoVinculacion entity, UpdateProyectoDto dto)
        {
            if (dto.PersonaID.HasValue && dto.PersonaID > 0)
                entity.PersonaID = dto.PersonaID.Value;

            if (dto.RecintoID.HasValue && dto.RecintoID > 0)
                entity.RecintoID = dto.RecintoID.Value;

            if (!string.IsNullOrWhiteSpace(dto.TituloProyecto))
                entity.TituloProyecto = dto.TituloProyecto;

            if (!string.IsNullOrWhiteSpace(dto.DescripcionGeneral))
                entity.DescripcionGeneral = dto.DescripcionGeneral;

            if (dto.FechaInicio.HasValue)
                entity.FechaInicio = dto.FechaInicio.Value;

            if (dto.FechaFin.HasValue)
                entity.FechaFin = dto.FechaFin.Value;

            if (dto.Ambito.HasValue && dto.Ambito > 0)
                entity.Ambito = dto.Ambito.Value;

            if (dto.Sector.HasValue && dto.Sector > 0)
                entity.Sector = dto.Sector.Value;

            entity.FechaModificacion = DateTime.Now;
        }
    }
}
