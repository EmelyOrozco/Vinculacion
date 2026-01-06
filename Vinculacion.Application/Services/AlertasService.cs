using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Application.Interfaces.Services.IProyectoVinculacionService;

namespace Vinculacion.Application.Services
{
    public class AlertasService: IAlertasService
    {
        private readonly IProyectoService _proyectoService;
        private readonly IActividadVinculacionService _actividadVinculacionService;
        public AlertasService(IProyectoService proyectoService, IActividadVinculacionService actividadVinculacionService)
        {
            _proyectoService = proyectoService;
            _actividadVinculacionService = actividadVinculacionService;
        }

        public async Task EnviarAlertaAsync()
        {
            var hoy = DateTime.Today;

            await _proyectoService.ProcesarProyectosAsync(hoy);
            await _actividadVinculacionService.ProcesarActividadesAsync(hoy);
        }
    }
}
