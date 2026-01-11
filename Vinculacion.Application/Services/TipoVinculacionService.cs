using Vinculacion.Application.Dtos;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.Application.Services
{
    public class TipoVinculacionService : ITipoVinculacionService
    {
        private readonly ITipoVinculacionRepository _repository;

        public TipoVinculacionService(ITipoVinculacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TipoVinculacionDto>> GetTiposProyectoAsync()
        {
            var tipos = await _repository.GetAllAsync();

            return tipos
                .Where(x => x.EsProyecto)
                .Select(x => new TipoVinculacionDto
                {
                    TipoVinculacionID = x.TipoVinculacionID,
                    Descripcion = x.Descripcion,
                    Detalle = x.Detalle,
                    EsProyecto = x.EsProyecto
                })
                .ToList();
        }
    }
}
