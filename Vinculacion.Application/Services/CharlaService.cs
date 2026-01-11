using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services
{
    public class CharlaService: ICharlaService
    {
        private readonly IActividadVinculacionRepository _actividadVinculacionRepository;
        private readonly ICharlaVinculacionRepository _charlaVinculacionRepository;
        public CharlaService(IActividadVinculacionRepository actividadVinculacionRepository, ICharlaVinculacionRepository charlaVinculacionRepository)
        {
            _actividadVinculacionRepository = actividadVinculacionRepository;
            _charlaVinculacionRepository = charlaVinculacionRepository;
        }

        public async Task<decimal> GetCharlasActivasFinalizadas(decimal charlaId)
        {
            var charlas = await _actividadVinculacionRepository.GetCharlasActivasFinalizadasAsync();

            if(!charlas.Any(x => x.ActividadId == charlaId))
            {
                throw new Exception("No se puede realizar la subida porque la charla no se encuentra activa o finalizada recientemente.");
            }

            var charlaID = charlas.Select(x => x.ActividadId).FirstOrDefault();

            return charlaID;
        }

        public async Task<List<string>> GetCharlasActivasFinalizadas()
        {
            var charlas = await _actividadVinculacionRepository.GetCharlasActivasFinalizadasAsync();

            var charlatitulo = charlas
                .Select(x => x.TituloActividad)
                .Where(titulo => titulo != null)
                .ToList()!;

            if (charlatitulo.Count == 0)
            {
                throw new Exception("No se encontraron títulos de charlas válidos.");
            }

            return charlatitulo!;
        }

        public async Task<OperationResult<List<CharlaVinculacion>>> GetAllCharlaVinculacion()
        {
            var result = await _charlaVinculacionRepository.GetAllAsync(l => true);

            if (!result.IsSuccess)
            {
                return OperationResult<List<CharlaVinculacion>>.Failure($"Error obteniendo las charlas de vinculación {result.Message}");
            }

            return OperationResult<List<CharlaVinculacion>>.Success("Charlas de vinculación: ", result.Data);
        }

    }
}
