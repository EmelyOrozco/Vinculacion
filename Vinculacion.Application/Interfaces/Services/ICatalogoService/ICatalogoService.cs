using Vinculacion.Application.Dtos.CatalogoDto;

namespace Vinculacion.Application.Interfaces.Services.ICatalogoService
{
    public interface ICatalogoService
    {
        Task<IEnumerable<CatalogoDto>> GetPaisesAsync();
        Task<IEnumerable<CatalogoDto>> GetRecintosAsync();
        Task<IEnumerable<CatalogoDto>> GetFacultadesAsync();
        Task<IEnumerable<CatalogoDto>> GetEscuelasByFacultadAsync(decimal facultadId);
        Task<IEnumerable<CatalogoDto>> GetCarrerasByEscuelaAsync(decimal escuelaId);
    }

}
