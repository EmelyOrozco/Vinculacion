using Microsoft.AspNetCore.Mvc;

namespace Vinculacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected decimal UsuarioId
        {
            get
            {
                var claim = User.FindFirst("UsuarioID");

                if (claim == null)
                    throw new UnauthorizedAccessException("Usuario no autenticado");

                return decimal.Parse(claim.Value);
            }
        }
    }
}
