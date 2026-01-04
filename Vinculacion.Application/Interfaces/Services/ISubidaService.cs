
using Vinculacion.Application.Dtos;

namespace Vinculacion.Application.Interfaces.Services
{
    public interface ISubidaService
    {
        Task EjecutarSubida(Stream archivo, CancellationToken cancellationToken);
        Task<ArchivoDescargaDto> GenerarPlantillaExcel(CancellationToken cancellationToken);
    }
}
