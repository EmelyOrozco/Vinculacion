using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Superusuario")]
    public class UsersController : ControllerBase
    {
       private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsersAddDto usersDto) 
        {
            var result = await _usersService.AddUserAsync(usersDto);

            if (!result.IsSuccess) 
            {
                return BadRequest(result); 
            }
            return Ok(result.Data);
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

        [HttpPut("{id}/Rol")]
        public async Task<IActionResult> UpdateUserRole(decimal id, [FromBody] UsersUpdateDto dto)
        {
            var result = await _usersService.UpdateUserRolAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPut("{id}/State")]
        public async Task<IActionResult> UpdateStateUser([FromBody] UsersUpdateDto usersUpdateDto, decimal id)
        {
            var result = await _usersService.UpdateUserStateAsync(id, usersUpdateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPut("{id}/Pass")]
        public async Task<IActionResult> UpdatePasswordUser([FromBody] UsersUpdateDto usersUpdateDto, decimal id)
        {
            var result = await _usersService.UpdateUserPasswordAsync(id, usersUpdateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }
    }
}
