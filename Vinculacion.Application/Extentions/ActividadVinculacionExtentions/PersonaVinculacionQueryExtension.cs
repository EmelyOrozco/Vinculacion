using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class PersonaVinculacionQueryExtension
    {
        public static PersonaVinculacionDto ToPersonaVinculacionDto(this PersonaVinculacion entity)
        {
            return new PersonaVinculacionDto
            {
                TipoPersonaID = entity.TipoPersonaID,
                RecintoID = entity.RecintoID,
                FacultadID = entity.FacultadID,
                EscuelaID = entity.EscuelaID,
                CarreraID = entity.CarreraID,
                NombreCompleto = entity.NombreCompleto,
                Correo = entity.Correo,
                TelefonoContacto = entity.TelefonoContacto,
                TipoRelacion = entity.TipoRelacion,
                Matricula = entity.Matricula,
                CodigoEmpleado = entity.CodigoEmpleado,
                AnoEgreso = entity.AnoEgreso,
                CargoEmpresa = entity.CargoEmpresa
            };
        }
    }
}