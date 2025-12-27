using Microsoft.AspNetCore.Mvc;
using Vinculacion.API.Models;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _authService.GenerateTokenAsync(login.CodigoEmpleado, login.Password);

            if (token is null)
            {
                return Unauthorized(new {Message = "Credenciales invalidas"});
            }
            return Ok(new { Token = token, expirationMinutes = 120});
        }
    }
}
