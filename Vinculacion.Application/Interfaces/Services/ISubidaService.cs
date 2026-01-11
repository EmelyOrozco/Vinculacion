
using Vinculacion.Application.Dtos;
using Vinculacion.Application.Enums;

namespace Vinculacion.Application.Interfaces.Services
{
    public interface ISubidaService
    {
        Task EjecutarSubida(TipoSubida tipoSubida, decimal contextoId, Stream archivo, CancellationToken cancellationToken);
        Task<ArchivoDescargaDto> GenerarPlantillaExcel(TipoSubida tipoSubida, CancellationToken cancellationToken);

    }
}
