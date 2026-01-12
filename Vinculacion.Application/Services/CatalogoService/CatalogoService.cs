using Vinculacion.Application.Dtos.CatalogoDto;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Repositories.CatalogoRepository;
using Vinculacion.Application.Interfaces.Services.ICatalogoService;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.CatalogoService
{
    public class CatalogoService : ICatalogoService
    {
        private readonly IPaisRepository _paisRepo;
        private readonly IRecintoRepository _recintoRepo;
        private readonly IFacultadRepository _facultadRepo;
        private readonly IEscuelaRepository _escuelaRepo;
        private readonly ICarreraRepository _carreraRepo;
        private readonly IRolRepository _rolRepo;
        private readonly IClasificacionEmpresaRepository _clasificacionEmpresaRepo;
        private readonly ITipoPersonaVinculacionRepository _tipoPersonaRepo;

        public CatalogoService(
        IPaisRepository paisRepo,
        IRecintoRepository recintoRepo,
        IFacultadRepository facultadRepo,
        IEscuelaRepository escuelaRepo,
        ICarreraRepository carreraRepo,
        IRolRepository rolRepo,
        IClasificacionEmpresaRepository clasificacionEmpresaRepository,
        ITipoPersonaVinculacionRepository tipoPersonaVinculacionRepository)
        {
            _paisRepo = paisRepo;
            _recintoRepo = recintoRepo;
            _facultadRepo = facultadRepo;
            _escuelaRepo = escuelaRepo;
            _carreraRepo = carreraRepo;
            _rolRepo = rolRepo;
            _clasificacionEmpresaRepo = clasificacionEmpresaRepository;
            _tipoPersonaRepo = tipoPersonaVinculacionRepository;
        }

        public async Task<IEnumerable<CatalogoDto>> GetPaisesAsync()
        {
            var result = await _paisRepo.GetAllAsync(x => true);

            if (!result.IsSuccess || result.Data == null)
                return Enumerable.Empty<CatalogoDto>();

            var data = result.Data as IEnumerable<Pais>;

            if (data == null)
                return Enumerable.Empty<CatalogoDto>();

            return data.Select(x => new CatalogoDto
            {
                Id = x.PaisID,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetRecintosAsync()
        {
            var data = await _recintoRepo.GetAllAsync();
            return data.Select(x => new CatalogoDto
            {
                Id = x.RecintoID,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetFacultadesAsync()
        {
            var data = await _facultadRepo.GetAllAsync();
            return data.Select(x => new CatalogoDto
            {
                Id = x.FacultadID,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetEscuelasByFacultadAsync(decimal facultadId)
        {
            var data = await _escuelaRepo.GetByFacultadAsync(facultadId);
            return data.Select(x => new CatalogoDto
            {
                Id = x.EscuelaID,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetCarrerasByEscuelaAsync(decimal escuelaId)
        {
            var data = await _carreraRepo.GetByEscuelaAsync(escuelaId);
            return data.Select(x => new CatalogoDto
            {
                Id = x.CarreraID,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetRolesAsync()
        {
            var data = await _rolRepo.GetAllAsync();

            return data.Select(x => new CatalogoDto
            {
                Id = x.Idrol,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetClasificacionesEmpresaAsync()
        {
            var data = await _clasificacionEmpresaRepo.GetAllAsync();

            return data.Select(x => new CatalogoDto
            {
                Id = x.ClasificacionID,
                Descripcion = x.Descripcion
            });
        }

        public async Task<IEnumerable<CatalogoDto>> GetTiposPersonaAsync()
        {
            var data = await _tipoPersonaRepo.GetAllAsync();

            return data.Select(x => new CatalogoDto
            {
                Id = x.TipoPersonaID,
                Descripcion = x.Descripcion
            });
        }
    }
}
