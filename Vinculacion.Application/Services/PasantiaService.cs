using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;


namespace Vinculacion.Application.Services
{
    public class PasantiaService: IPasantiaService
    {
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IPasantiaVinculacionRepository _pasantiaVinculacionRepository;
        public PasantiaService(IProyectoRepository proyectoRepository, IPasantiaVinculacionRepository pasantiaVinculacionRepository)
        {
            _proyectoRepository = proyectoRepository;
            _pasantiaVinculacionRepository = pasantiaVinculacionRepository;
        }

        public async Task<decimal> GetPasantiasActivasFinalizadas(decimal pasantiaID)
        {
            var charlas = await _proyectoRepository.GetPasantiasActivasFinalizadasAsync();

            if (!charlas.Any(x => x.ProyectoID == pasantiaID))
            {
                throw new Exception("No se puede realizar la subida porque la pasantia no se encuentra activa o finalizada recientemente.");
            }

            var charlaID = charlas.Select(x => x.ProyectoID).FirstOrDefault();

            return charlaID;
        }


        public async Task<List<string>> GetPasantiasActivasFinalizadas()
        {
            var pasantias = await _proyectoRepository.GetPasantiasActivasFinalizadasAsync();
            var pasantia = pasantias
                .Select(x => x.TituloProyecto)
                .Where(t => t != null)
                .Cast<string>()
                .ToList();

            if (pasantia is null)
            {
                throw new Exception("No se encontraron títulos de pasantía válidos.");
            }

            return pasantia;
        }

        public async Task<OperationResult<List<PasantiaVinculacion>>> GetAllPasantiaVinculacion()
        {
            var result = await _pasantiaVinculacionRepository.GetAllAsync(l => true);

            if (!result.IsSuccess)
            {
                return OperationResult<List<PasantiaVinculacion>>.Failure($"Error obteniendo las pasantías de vinculación {result.Message}");
            }

            return OperationResult<List<PasantiaVinculacion>>.Success("Pasantías de vinculación: ", result.Data);
        }



    }
}
