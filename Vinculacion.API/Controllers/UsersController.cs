using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Dtos.UsuarioSistemaDto;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsersDto usersDto) 
        {
            var result = await _usersService.AddUserAsync(usersDto);

            if (!result.IsSuccess) 
            {
                return BadRequest(result); 
            }
            return Ok(result.Data);
        }
    }
}
