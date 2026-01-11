using Microsoft.AspNetCore.Mvc;
using Vinculacion.API.Controllers;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Application.Interfaces.Services.IProyectoVinculacionService;

[ApiController]
[Route("api/[controller]")]
public class ProyectoController : BaseController
{
    private readonly IProyectoService _proyectoService;

    public ProyectoController(IProyectoService proyectoService)
    {
        _proyectoService = proyectoService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddProyectoDto dto)
    {
        var usuarioId = UsuarioId;
        var result = await _proyectoService.AddProyectoAsync(dto, usuarioId);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _proyectoService.GetProyectosAsync();

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(decimal id)
    {
        var result = await _proyectoService.GetProyectoByIdAsync(id);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProyecto(decimal id, [FromBody] UpdateProyectoDto dto)
    {
        var usuarioId = UsuarioId;
        var result = await _proyectoService.UpdateProyectoAsync(id, dto, usuarioId);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    /// <summary>
    /// Vincula una actividad existente a un proyecto.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="actividadId">ID de la actividad.</param>
    /// <response code="200">Actividad vinculada correctamente.</response>
    /// <response code="400">La actividad ya pertenece a otro proyecto o es inválida.</response>

    [HttpPost("{proyectoId}/Actividades/{actividadId}")]
    public async Task<IActionResult> AddActividadToProyecto(decimal proyectoId,decimal actividadId)
    {
        var usuarioId = UsuarioId;
        var result = await _proyectoService
            .AddActividadToProyectoAsync(proyectoId, actividadId, usuarioId);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    /// <summary>
    /// Vincula varias actividades a la vez a un proyecto.
    /// </summary>
    /// <param name="proyectoId">ID del proyecto.</param>
    /// <param name="dto">Lista de IDs de actividades.</param>
    /// <response code="200">Actividades vinculadas correctamente.</response>
    /// <response code="400">Alguna actividad no es válida o ya pertenece a otro proyecto.</response>

    [HttpPost("{proyectoId}/Actividades")]
    public async Task<IActionResult> AddActividadesToProyecto(decimal proyectoId,[FromBody] AddActividadesToProyectoDto dto)
    {
        var usuarioId = UsuarioId;
        var result = await _proyectoService
            .AddActividadesToProyectoAsync(proyectoId, dto, usuarioId);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    /// <summary>
    /// Obtiene las actividades vinculadas a un proyecto.
    /// </summary>
    [HttpGet("{proyectoId}/Actividades")]
    public async Task<IActionResult> GetActividadesByProyecto(decimal proyectoId)
    {
        var result = await _proyectoService
            .GetActividadesByProyectoAsync(proyectoId);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }

    /// <summary>
    /// Obtiene las actividades disponibles para ser asociadas a un proyecto.
    /// </summary>
    [HttpGet("ActividadesDisponibles")]
    public async Task<IActionResult> GetActividadesDisponibles()
    {
        var result = await _proyectoService.GetActividadesDisponiblesAsync();

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(new{ message = result.Message, data = result.Data});
    }

    /// <summary>
    /// Obtiene las actividades disponibles para vincular a un proyecto,
    /// filtradas por el actor externo del proyecto.
    /// </summary>
    [HttpGet("{proyectoId}/ActividadesDisponibles")]
    public async Task<IActionResult> GetActividadesDisponiblesByProyecto(decimal proyectoId)
    {
        var result = await _proyectoService
            .GetActividadesDisponiblesByProyectoAsync(proyectoId);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }


}
