using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;

namespace Vinculacion.Application.Services.ActividadVinculacionService
{
    public class ActividadVinculacionService: IActividadVinculacionService
    {
        private readonly IActividadVinculacionRepository _actividadVinculacionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;
        
        public ActividadVinculacionService(IActividadVinculacionRepository actividadVinculacionRepository,
            IUnitOfWork unitOfWork,
            IUsersRepository usersRepository,
            IEmailService emailService)
        {
            _actividadVinculacionRepository = actividadVinculacionRepository;
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
            _emailService = emailService;
        }

        public async Task<OperationResult<ActividadVinculacionDto>> AddActividadVinculacion(ActividadVinculacionDto actividadVinculacionDto)
        {
            var actividadVinculacion = actividadVinculacionDto.ToActividadVinculacionFromDto();

            if (actividadVinculacionDto.Subtareas is not null)
            {
                actividadVinculacion.Subtareas = new List<ActividadSubtareas>();
                foreach (var subtareaDto in actividadVinculacionDto.Subtareas)
                {
                    actividadVinculacion.Subtareas.Add(subtareaDto.ToActividadSubtareasFromDto());

                }
            }
            var actividad = await _actividadVinculacionRepository.AddAsync(actividadVinculacion);
            var result = await _unitOfWork.SaveChangesAsync();
            
            return OperationResult<ActividadVinculacionDto>.Success("Actividad Vinculacion agregada correctamente ", result);
        }

        public async Task<OperationResult<List<ActividadVinculacionDto>>> GetAllAsync()
        {
            var result = await _actividadVinculacionRepository.GetAllWithSubtareasAsync();

            if (!result.IsSuccess)
                return OperationResult<List<ActividadVinculacionDto>>
                    .Failure("Error obteniendo actividades");

            var entidades = (List<ActividadVinculacion>)result.Data;

            var data = entidades
                .Select(x => x.ToActividadVinculacionDto())
                .ToList();

            return OperationResult<List<ActividadVinculacionDto>>
                .Success("Actividades obtenidas", data);
        }

        public async Task<OperationResult<ActividadVinculacionDto>> GetByIdAsync(decimal id)
        {
            var result = await _actividadVinculacionRepository.GetByIdWithSubtareasAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<ActividadVinculacionDto>
                    .Failure("Actividad no encontrada");

            var entidad = (ActividadVinculacion)result.Data;

            var dto = entidad.ToActividadVinculacionDto();

            return OperationResult<ActividadVinculacionDto>
                .Success("Actividad encontrada", dto);
        }


        public async Task<OperationResult<bool>> UpdateAsync(decimal id, ActividadVinculacionDto dto)
        {
            var result = await _actividadVinculacionRepository.GetByIdWithSubtareasAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return OperationResult<bool>.Failure("Actividad no encontrada");

            var entity = result.Data;

            if (dto.ActorExternoId.HasValue && dto.ActorExternoId > 0)
                entity.ActorExternoId = dto.ActorExternoId.Value;

            if (dto.RecintoId.HasValue && dto.RecintoId > 0)
                entity.RecintoId = dto.RecintoId.Value;

            if (dto.TipoVinculacionId.HasValue && dto.TipoVinculacionId > 0)
                entity.TipoVinculacionId = dto.TipoVinculacionId.Value;

            if (dto.PersonaId.HasValue && dto.PersonaId > 0)
                entity.PersonaId = dto.PersonaId.Value;

            if (dto.EstadoId.HasValue && dto.EstadoId > 0)
                entity.EstadoId = dto.EstadoId.Value;

            if (!string.IsNullOrWhiteSpace(dto.TituloActividad))
                entity.TituloActividad = dto.TituloActividad;

            if (!string.IsNullOrWhiteSpace(dto.DescripcionActividad))
                entity.DescripcionActividad = dto.DescripcionActividad;

            if (dto.Modalidad.HasValue && dto.Modalidad > 0)
                entity.Modalidad = dto.Modalidad.Value;

            if (!string.IsNullOrWhiteSpace(dto.Lugar))
                entity.Lugar = dto.Lugar;

            if (dto.FechaHoraEvento.HasValue)
                entity.FechaHoraEvento = dto.FechaHoraEvento;

            if (dto.Ambito.HasValue && dto.Ambito > 0)
                entity.Ambito = dto.Ambito.Value;

            if (dto.Sector.HasValue && dto.Sector > 0)
                entity.Sector = dto.Sector.Value;

            if (dto.Subtareas != null)
            {
                entity.Subtareas.Clear();

                foreach (var subtareaDto in dto.Subtareas)
                {
                    entity.Subtareas.Add(subtareaDto.ToActividadSubtareasFromDto());
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success("Actividad actualizada correctamente", true);
        }


        public async Task ProcesarActividadesAsync(DateTime hoy)
        {
            var actividades = await _actividadVinculacionRepository.GetActividadEstatusActivo();

            foreach(var actividad in actividades)
            {
                double? diasFaltantes = null;

                if (actividad.FechaHoraEvento.HasValue)
                {
                    diasFaltantes = (actividad.FechaHoraEvento.Value - hoy).TotalDays;
                }

                if (!FuncionesService.DebeNotificar(diasFaltantes))
                    continue;

                var titulo = FuncionesService.ObtenerTitulo("Actividad", diasFaltantes);
                var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px;'>
                    <div style='max-width:600px; background-color:#ffffff; padding:20px; border-radius:6px;'>
                        <h2 style='color:#333;'>🔔 Actividad próxima a vencer</h2>

                        <p>Hola,</p>

                        <p>
                            Te informamos que la actividad 
                            <strong>{actividad.TituloActividad}</strong> tiene como fecha de finalización:
                        </p>

                        <p style='font-size:16px;'>
                            📅 <strong>{actividad.FechaHoraEvento:dd/MM/yyyy}</strong>
                        </p>

                        <p>
                            Por favor, revisa el estado de la actividad y realiza las acciones correspondientes.
                        </p>

                        <hr />

                        <p style='font-size:12px; color:#777;'>
                            Sistema de Vinculación Universitaria<br/>
                            UNPHU
                        </p>
                    </div>
                </body>
                </html>";

                var correoUsuarios = await _usersRepository.GetCorreoUsuariosAlertas();

                foreach (var usuario in correoUsuarios)
                {
                    if (!string.IsNullOrWhiteSpace(usuario.CorreoInstitucional))
                    {
                        await _emailService.SendEmail(usuario.CorreoInstitucional, titulo, body);
                    }
                }

            }
        }
    }
}
