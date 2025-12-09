using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Extentions.UsuarioSistemaExtentions
{
    public static class UsuariosExtention
    {
        public static Usuario ToUsersValidationFromDto(this UsersDto usuarioDto)
        {
            return new Usuario
            {
                CodigoEmpleado = usuarioDto.Usuario,
                PasswordHash = usuarioDto.Password,
                Idrol = usuarioDto.Idrol,
                EstadoId = usuarioDto.EstadoId,
                NombreCompleto = usuarioDto.NombreCompleto,
                CorreoInstitucional = usuarioDto.CorreoInstitucional
            };
        }

        public static UsersDto ToUsersDtoFromEntity(this Usuario usuario)
        {
            return new UsersDto
            {
                Usuario = usuario.CodigoEmpleado,
                Password = usuario.PasswordHash,
                Idrol = usuario.Idrol,
                EstadoId = usuario.EstadoId,
                NombreCompleto = usuario.NombreCompleto,
                CorreoInstitucional = usuario.CorreoInstitucional
            };
        }
    }
}
