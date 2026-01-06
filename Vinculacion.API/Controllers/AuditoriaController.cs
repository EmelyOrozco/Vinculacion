using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vinculacion.Application.Interfaces.Repositories;

namespace Vinculacion.API.Controllers
{
    [Authorize(Roles = "Superusuario")]
    [ApiController]
    [Route("api/auditoria")]
    public class AuditoriaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _unitOfWork.Auditoria.GetAllAsync();
            return Ok(data);
        }
    }

}
