using System.Text.Json;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class PersonaVinculacionService: IPersonaVinculacionService
    {
        private readonly IPersonaVinculacionRepository _personaVinculacionRepository;
        public PersonaVinculacionService(IPersonaVinculacionRepository personaVinculacionRepository)
        {
            _personaVinculacionRepository = personaVinculacionRepository;
        }

        public async Task<decimal> AddPersonaVinculacion(PersonaVinculacionRequest request)
        {
            PersonaVinculacion persona = request.TipoPersonaId switch
            {
                1 => JsonSerializer.Deserialize<EstudianteDto>(JsonSerializer.Serialize(request)).ToEstudianteFromDto(),
            };
            return 1;
        }
    }
}
