using Vinculacion.Application.Dtos;

namespace Vinculacion.Application.Interfaces.Services
{
    public interface ITipoVinculacionService
    {
        Task<List<TipoVinculacionDto>> GetTiposProyectoAsync();
    }
}
