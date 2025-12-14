using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.UsuarioSistemaExtentions
{
    public static class UsuariosExtention
    {
        public static UsersDto ToUsersDtoFromEntity(this Usuario usuario)
        {
            return new UsersDto
            {
                Usuario = usuario.CodigoEmpleado,
                Password = usuario.PasswordHash,
                Idrol = usuario.Idrol,
                EstadoId = usuario.EstadoId,
                NombreCompleto = usuario.NombreCompleto,
                CorreoInstitucional = usuario.CorreoInstitucional,
                NombreRol = usuario.rol.Descripcion
            };
        }

        public static Usuario ToUsuarioFromAddDto(this UsersAddDto usuarioDto)
        {
            return new Usuario
            {
                CodigoEmpleado = usuarioDto.CodigoEmpleado,
                Idrol = usuarioDto.Idrol,
                EstadoId = 1,
                NombreCompleto = usuarioDto.NombreCompleto,
                CorreoInstitucional = usuarioDto.CorreoInstitucional,
                FechaRegistro = DateTime.UtcNow
            };
        }

        public static Usuario ToUsuarioFromUpdateDto(this UsersUpdateDto usuarioDto)
        {
            return new Usuario
            {
                Idrol = usuarioDto.Idrol,
                EstadoId = usuarioDto.EstadoId,
                PasswordHash = usuarioDto.PasswordHash,
                FechaModificacion = DateTime.UtcNow
            };
        }
    }
}
