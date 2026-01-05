using Vinculacion.Application.Dtos.EstadoDto;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<OperationResult<List<EstadoDto>>> GetEstadosPorTablaAsync(string tablaEstado)
        {
            var estados = await _estadoRepository.GetByTablaAsync(tablaEstado);

            if (!estados.Any())
            {
                return OperationResult<List<EstadoDto>>
                    .Failure("No existen estados para la tabla solicitada");
            }

            var data = estados.Select(e => new EstadoDto
            {
                EstadoID = e.EstadoID,
                Descripcion = e.Descripcion
            }).ToList();

            return OperationResult<List<EstadoDto>>
                .Success("Estados obtenidos correctamente", data);
        }

        public async Task<OperationResult<List<EstadoGeneralDto>>> GetAllAsync()
        {
            var estados = await _estadoRepository.GetAllAsync();

            var data = estados.Select(e => new EstadoGeneralDto
            {
                EstadoID = e.EstadoID,
                Descripcion = e.Descripcion,
                TablaEstado = e.TablaEstado
            }).ToList();

            return OperationResult<List<EstadoGeneralDto>>
                .Success("Estados obtenidos correctamente", data);
        }

    }

}
