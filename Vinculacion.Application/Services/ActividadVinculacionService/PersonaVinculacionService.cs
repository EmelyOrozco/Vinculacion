using FluentValidation;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class PersonaVinculacionService: IPersonaVinculacionService
    {
        private readonly IPersonaVinculacionRepository _personaVinculacionRepository;
        private readonly IValidator<PersonaVinculacionDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public PersonaVinculacionService(IPersonaVinculacionRepository personaVinculacionRepository,
            IValidator<PersonaVinculacionDto> validator,
            IUnitOfWork unitOfWork)
        {
            _personaVinculacionRepository = personaVinculacionRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<PersonaVinculacionDto>> AddPersonaVinculacion(PersonaVinculacionDto personaVinculacionDto)
        {
            var personaVinculacion = personaVinculacionDto.ToPersonaVinculacionFromDto();

            var result = await _validator.ValidateAsync(personaVinculacionDto);

            if (!result.IsValid)
            {
                return OperationResult<PersonaVinculacionDto>.Failure("Error: ", result.Errors.Select(x => x.ErrorMessage));
            }

            var guardar = await _personaVinculacionRepository.AddAsync(personaVinculacion);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<PersonaVinculacionDto>.Success("Persona vincunlante guardada correctamente", personaVinculacionDto);
        }
    }
}
