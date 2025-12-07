using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class PersonaVinculacionExtention
    {
        public static PersonaVinculacion ToPersonaVinculacionFromDto(this PersonaVinculacionDto personaVinculacionDto)
        {
            return new PersonaVinculacion
            { 
                TipoPersonaID = personaVinculacionDto.TipoPersonaID,
                NombreCompleto = personaVinculacionDto.NombreCompleto,
                Correo = personaVinculacionDto.Correo,
                TelefonoContacto = personaVinculacionDto.TelefonoContacto,
                Matricula = personaVinculacionDto.Matricula,
                RecintoID = personaVinculacionDto.RecintoID,
                EscuelaID = personaVinculacionDto.EscuelaID,
                CarreraID = personaVinculacionDto.CarreraID,
                TipoRelacion = personaVinculacionDto.TipoRelacion,
                CodigoEmpleado = personaVinculacionDto.CodigoEmpleado,
                AnoEgreso = personaVinculacionDto.AnoEgreso,
                CargoEmpresa = personaVinculacionDto.CargoEmpresa
            };
        }
       
    }
}
