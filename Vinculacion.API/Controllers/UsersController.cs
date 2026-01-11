using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Superusuario")]
    public class UsersController : BaseController
    {
       private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsersAddDto usersDto) 
        {
            var usuarioId = UsuarioId;
            var result = await _usersService.AddUserAsync(usersDto, usuarioId);

            if (!result.IsSuccess) 
            {
                return BadRequest(result); 
            }
            return Ok(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _usersService.GetAllUsersAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(decimal id)
        {
            var result = await _usersService.GetUserById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Data);
        }

        [Authorize(Roles = "Superusuario")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UsersUpdateDto usersUpdateDto, decimal id)
        {
            var usuarioId = UsuarioId;
            var result = await _usersService.UpdateUserAsync(usersUpdateDto, id, usuarioId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPut("{id}/Rol")]
        public async Task<IActionResult> UpdateUserRole(decimal id, [FromBody] UsersUpdateRolDto dto)
        {
            var usuarioId = UsuarioId;
            var result = await _usersService.UpdateUserRolAsync(id, dto, usuarioId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}/State")]
        public async Task<IActionResult> UpdateStateUser([FromBody] UsersUpdateStateDto usersUpdateDto, decimal id)
        {
            var usuarioId = UsuarioId;
            var result = await _usersService.UpdateUserStateAsync(id, usersUpdateDto, usuarioId);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPut("{id}/Pass")]
        public async Task<IActionResult> UpdatePasswordUser([FromBody] UsersUpdatePassDto usersUpdateDto, decimal id)
        {
            var usuarioId = UsuarioId;
            var result = await _usersService.UpdateUserPasswordAsync(id, usersUpdateDto, usuarioId);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
