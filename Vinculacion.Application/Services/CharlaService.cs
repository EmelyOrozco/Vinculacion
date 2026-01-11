using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.Application.Services
{
    public class CharlaService: ICharlaService
    {
        private readonly IActividadVinculacionRepository _actividadVinculacionRepository;
        public CharlaService(IActividadVinculacionRepository actividadVinculacionRepository)
        {
            _actividadVinculacionRepository = actividadVinculacionRepository;
        }

        public async Task<decimal> GetCharlasActivasFinalizadas(decimal charlaId)
        {
            var charlas = await _actividadVinculacionRepository.GetCharlasActivasFinalizadasAsync();

            if(!charlas.Any(x => x.ActividadId == charlaId))
            {
                throw new Exception("No se puede realizar la subida porque la charla se encuentra activa o finalizada recientemente.");
            }

            var charlaID = charlas.Select(x => x.ActividadId).FirstOrDefault();

            return charlaID;
        }
    }
}
