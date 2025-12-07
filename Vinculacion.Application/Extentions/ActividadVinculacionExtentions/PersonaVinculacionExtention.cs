using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.ActividadVinculacionExtentions
{
    public static class PersonaVinculacionExtention
    {
        public static PersonaVinculacion ToEstudianteFromDto(this EstudianteDto estudianteDto)
        {
            return new PersonaVinculacion
            { 
                TipoPersonaID = 1,
                NombreCompleto = estudianteDto.NombreCompleto,
                Correo = estudianteDto.Correo,
                TelefonoContacto = estudianteDto.TelefonoContacto,
                Matricula = estudianteDto.Matricula,
                RecintoID = estudianteDto.RecintoID,
                EscuelaID = estudianteDto.EscuelaID,
                CarreraID = estudianteDto.CarreraID
            };
        }
        public static PersonaVinculacion ToDocenteFromDto(this DocenteDto docenteDto)
        {
            return new PersonaVinculacion
            { 
                TipoPersonaID = 2,
                NombreCompleto = docenteDto.NombreCompleto,
                Correo = docenteDto.Correo,
                TelefonoContacto = docenteDto.TelefonoContacto,
                CodigoEmpleado = docenteDto.CodigoEmpleado,
                RecintoID = docenteDto.RecintoID,
                EscuelaID = docenteDto.EscuelaID,
                CarreraID = docenteDto.CarreraID
            };
        }

        public static PersonaVinculacion ToEgresadoFromDto(this EgresadoDto egresadoDto)
        {
            return new PersonaVinculacion
            { 
                TipoPersonaID = 4,
                NombreCompleto = egresadoDto.NombreCompleto,
                Correo = egresadoDto.Correo,
                TelefonoContacto = egresadoDto.TelefonoContacto,
                AnoEgreso = egresadoDto.AnoEgreso,
                RecintoID = egresadoDto.RecintoID,
                EscuelaID = egresadoDto.EscuelaID,
                CarreraID = egresadoDto.CarreraID
            };
        }

        public static PersonaVinculacion ToEmpleadoFromDto(this EjecutivoDto ejecutivoDto)
        {
            return new PersonaVinculacion
            { 
                TipoPersonaID = 5,
                NombreCompleto = ejecutivoDto.NombreCompleto,
                Correo = ejecutivoDto.Correo,
                TelefonoContacto = ejecutivoDto.TelefonoContacto,
                CargoEmpresa = ejecutivoDto.CargoEmpresa
            };
        }
    }
}
