using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Interfaces.Repositories.CatalogoRepository
{
    public interface IClasificacionEmpresaRepository
    {
        Task<IEnumerable<ClasificacionEmpresa>> GetAllAsync();
    }
}