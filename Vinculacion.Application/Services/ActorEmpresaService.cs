
using Microsoft.Extensions.Logging;
using Vinculacion.Application.Features.ActorVinculacion.Dtos;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services
{
    public class ActorEmpresaService: IActorEmpresaService
    {
        private readonly ILogger<ActorEmpresaService> _logger;
        private readonly IActorEmpresaRepository _actorEmpresaRepository;
        public ActorEmpresaService(ILogger<ActorEmpresaService> logger, IActorEmpresaRepository actorEmpresaRepository)
        {
            _logger = logger;
            _actorEmpresaRepository = actorEmpresaRepository;
        }

        public async Task<OperationResult<AddActorEmpresaDto>> CreateAsync(AddActorEmpresaDto createActorEmpresaDto)
        {
            return OperationResult<AddActorEmpresaDto>.Success("Empresa añadida correctamente", createActorEmpresaDto);
        }
    }
}
