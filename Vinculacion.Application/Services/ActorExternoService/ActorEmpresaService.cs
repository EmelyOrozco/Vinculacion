
using Microsoft.Extensions.Logging;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services.ActorExternoService
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
