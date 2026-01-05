using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vinculacion.Application.Interfaces.Services;

namespace Vinculacion.Persistence.Repositories
{
    public class AlertasBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AlertasBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var alertasRepository = scope.ServiceProvider.GetRequiredService<IAlertasService>();

                    await alertasRepository.EnviarAlertaAsync();
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }

        }
    }
}
