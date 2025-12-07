using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Services.IActorExternoService
{
    public interface IActorEmpresaService
    {
        Task<OperationResult<AddActorEmpresaDto>> AddActorEmpresaAsync(AddActorEmpresaDto addActorEmpresaDto);
    }
}
