using FluentValidation;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.ActividadSubtareas;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Application.Extentions.ActividadVinculacionExtentions;
using Vinculacion.Application.Extentions.ProyectoVinculacionExtentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IProyectoVinculacionService;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Domain.Base;
using Vinculacion.Domain.Entities;
using System.Text.Json;
using Vinculacion.Application.Contante;

namespace Vinculacion.Application.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IValidator<AddProyectoDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProyectoDto> _updateValidator;
        private readonly IActividadVinculacionRepository _actividadRepository;
        private readonly IProyectoActividadRepository _proyectoActividadRepository;
        private readonly IValidator<AddActividadesToProyectoDto> _addActividadesValidator;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;
        private readonly ITipoVinculacionRepository _tipoVinculacionRepository;

        public ProyectoService(
            IProyectoRepository proyectoRepository,
            IValidator<AddProyectoDto> validator,
            IUnitOfWork unitOfWork,
            IValidator<UpdateProyectoDto> updateValidator,
            IActividadVinculacionRepository actividadRepository,
            IProyectoActividadRepository proyectoActividadRepository,
            IValidator<AddActividadesToProyectoDto> addActividadesValidator,
            IUsersRepository usersRepository,
            IEmailService emailService,
            ITipoVinculacionRepository tipoVinculacionRepository)
        {
            _proyectoRepository = proyectoRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _updateValidator = updateValidator;
            _actividadRepository = actividadRepository;
            _proyectoActividadRepository = proyectoActividadRepository;
            _addActividadesValidator = addActividadesValidator;
            _usersRepository = usersRepository;
            _emailService = emailService;
            _tipoVinculacionRepository = tipoVinculacionRepository;
        }

        public async Task<OperationResult<AddProyectoDto>> AddProyectoAsync(AddProyectoDto request, decimal usuarioId)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return OperationResult<AddProyectoDto>.Failure(
                    "Error:",
                    validation.Errors.Select(e => e.ErrorMessage)
                );
            }

            var tipo = await _tipoVinculacionRepository.GetByIdAsync(request.TipoVinculacionID);
            if (tipo == null || !tipo.EsProyecto)
            {
                return OperationResult<AddProyectoDto>.Failure(
                    "Solo se permiten Pasantías o Servicio Social para proyectos."
                );
            }

            var entity = request.ToProyectoFromAddDto();

            entity.EstadoID = DeterminarEstadoProyecto(entity);
            entity.FechaRegistro = DateTime.UtcNow;

            await _proyectoRepository.AddAsync(entity);
            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Crear",
                Entidad = "ProyectoVinculacion",
                EntidadId = null
            });
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<AddProyectoDto>.Success(
                "Proyecto creado correctamente",
                request
            );
        }

        public async Task<OperationResult<List<ProyectoResponseDto>>> GetProyectosAsync()
        {
            try
            {
                var proyectos = await _proyectoRepository.GetAllWithActividadesAsync();

                var data = proyectos
                    .Select(p => p.ToProyectoResponseDto())
                    .ToList();

                return OperationResult<List<ProyectoResponseDto>>.Success(
                    "Proyectos obtenidos correctamente",
                    data
                );
            }
            catch (Exception)
            {
                return OperationResult<List<ProyectoResponseDto>>.Failure(
                    "Error al obtener los proyectos."
                );
            }
        }
        public async Task<OperationResult<ProyectoResponseDto>> GetProyectoByIdAsync(decimal proyectoId)
        {
            try
            {
                var proyecto = await _proyectoRepository.GetByIdWithActividadesAsync(proyectoId);

                if (proyecto == null)
                {
                    return OperationResult<ProyectoResponseDto>.Failure(
                        "El proyecto no existe",
                        null
                    );
                }

                var data = proyecto.ToProyectoResponseDto();

                return OperationResult<ProyectoResponseDto>.Success(
                    "Proyecto obtenido correctamente",
                    data
                );
            }
            catch (Exception)
            {
                return OperationResult<ProyectoResponseDto>.Failure(
                    "Error al obtener el proyecto"
                );
            }
        }
        public async Task<OperationResult<bool>> UpdateProyectoAsync(decimal proyectoId,UpdateProyectoDto dto, decimal usuarioId)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return OperationResult<bool>.Failure(
                string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage))
            );
            }

            var proyectoResult = await _proyectoRepository.GetByIdAsync(proyectoId);

            if (!proyectoResult.IsSuccess || proyectoResult.Data == null)
            {
                return OperationResult<bool>.Failure("El proyecto no existe", null);
            }

            var proyecto = proyectoResult.Data;

            var antes = JsonSerializer.Serialize(new
            {
                proyecto.TituloProyecto,
                proyecto.DescripcionGeneral,
                proyecto.FechaInicio,
                proyecto.FechaFin,
                proyecto.RecintoID,
                proyecto.PersonaID
            });

            if (dto.TipoVinculacionID.HasValue)
            {
                var tipo = await _tipoVinculacionRepository
                    .GetByIdAsync(dto.TipoVinculacionID.Value);

                if (tipo == null || !tipo.EsProyecto)
                {
                    return OperationResult<bool>.Failure(
                        "No se puede asignar un tipo de vinculación que no sea de proyecto."
                    );
                }
            }

            ProyectoVinculacionUpdateExtention.UpdateFromDto(proyecto, dto);

            if (dto.EstadoID == EstadosProyecto.Deshabilitado)
            {
                proyecto.EstadoID = EstadosProyecto.Deshabilitado;
            }
            else
            {
                proyecto.EstadoID = DeterminarEstadoProyecto(proyecto);
            }


            var updateResult = await _proyectoRepository.Update(proyecto);
            if (!updateResult.IsSuccess)
            {
                return OperationResult<bool>.Failure(updateResult.Message, null);
            }

            var despues = JsonSerializer.Serialize(new
            {
                proyecto.TituloProyecto,
                proyecto.DescripcionGeneral,
                proyecto.FechaInicio,
                proyecto.FechaFin,
                proyecto.RecintoID,
                proyecto.PersonaID
            });


            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Actualizar",
                Entidad = "ProyectoVinculacion",
                EntidadId = proyectoId,
                DetalleAntes = antes,
                DetalleDespues = despues
            });

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success(
                "Proyecto actualizado correctamente"
            );
        }

        public async Task<OperationResult<bool>> AddActividadToProyectoAsync(decimal proyectoId, decimal actividadId, decimal usuarioId)
        {
            var proyectoResult = await _proyectoRepository.GetByIdAsync(proyectoId);
            if (!proyectoResult.IsSuccess || proyectoResult.Data == null)
            {
                return OperationResult<bool>.Failure("El proyecto no existe", null);
            }

            var proyecto = proyectoResult.Data;

            var actividadResult = await _actividadRepository.GetByIdAsync(actividadId);
            if (!actividadResult.IsSuccess || actividadResult.Data == null)
            {
                return OperationResult<bool>.Failure("La actividad no existe", null);
            }

            if (await _proyectoActividadRepository.ExistsActividadInAnyProyecto(actividadId))
            {
                return OperationResult<bool>.Failure(
                    "La actividad ya pertenece a otro proyecto",
                    null
                );
            }

            var relacion = new ProyectoActividad
            {
                ProyectoID = proyectoId,
                ActividadID = actividadId
            };

            await _proyectoActividadRepository.AddAsync(relacion);

            proyecto.EstadoID = DeterminarEstadoProyecto(proyecto);
            await _proyectoRepository.Update(proyecto);

            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Crear",
                Entidad = "ActividadToProyecto",
                EntidadId = null
            });
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success(
                "Actividad vinculada al proyecto correctamente",
                true
            );
        }

        public async Task<OperationResult<bool>> AddActividadesToProyectoAsync(decimal proyectoId, AddActividadesToProyectoDto dto, decimal usuarioId)
        {
            var validation = await _addActividadesValidator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return OperationResult<bool>.Failure(
                    "Error:",
                    validation.Errors.Select(e => e.ErrorMessage)
                );
            }

            var proyectoResult = await _proyectoRepository.GetByIdAsync(proyectoId);
            if (!proyectoResult.IsSuccess || proyectoResult.Data == null)
            {
                return OperationResult<bool>.Failure("El proyecto no existe", null);
            }

            var proyecto = proyectoResult.Data;

            var cantidadActual =
                await _proyectoActividadRepository.CountActividadesByProyecto(proyectoId);

            foreach (var actividadId in dto.ActividadesIds.Distinct())
            {
                var actividadResult = await _actividadRepository.GetByIdAsync(actividadId);
                if (!actividadResult.IsSuccess || actividadResult.Data == null)
                {
                    return OperationResult<bool>.Failure(
                        $"La actividad {actividadId} no existe",
                        null
                    );
                }

                if (await _proyectoActividadRepository
                    .ExistsActividadInAnyProyecto(actividadId))
                {
                    return OperationResult<bool>.Failure(
                        $"La actividad {actividadId} ya pertenece a otro proyecto",
                        null
                    );
                }

                await _proyectoActividadRepository.AddAsync(new ProyectoActividad
                {
                    ProyectoID = proyectoId,
                    ActividadID = actividadId
                });
            }

            if (cantidadActual == 0 && proyecto.EstadoID == 4)
            {
                proyecto.EstadoID = 1;
                proyecto.FechaModificacion = DateTime.Now;
                await _proyectoRepository.Update(proyecto);
            }
            await _unitOfWork.Auditoria.RegistrarAsync(new Auditoria
            {
                UsuarioID = usuarioId,
                FechaHora = DateTime.UtcNow,
                Accion = "Crear",
                Entidad = "ActividadesToProyecto",
                EntidadId = null
            });
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<bool>.Success(
                "Actividades vinculadas al proyecto correctamente",
                true
            );
        }

        public async Task<OperationResult<List<ActividadVinculacionDto>>>GetActividadesByProyectoAsync(decimal proyectoId)
        {
            var actividades = await _actividadRepository.GetActividadesByProyectoId(proyectoId);

            var response = actividades.Select(a => a.ToActividadVinculacionDto()).ToList();

            return OperationResult<List<ActividadVinculacionDto>>
                .Success("Actividades del proyecto obtenidas", response);
        }

        public async Task<OperationResult<List<ActividadVinculacionDto>>>GetActividadesDisponiblesByProyectoAsync(decimal proyectoId)
        {
            var proyectoResult = await _proyectoRepository.GetByIdAsync(proyectoId);
            if (!proyectoResult.IsSuccess || proyectoResult.Data == null)
            {
                return OperationResult<List<ActividadVinculacionDto>>
                    .Failure("El proyecto no existe", null);
            }

            var proyecto = proyectoResult.Data;

            if (proyecto.ActorExternoID == null)
            {
                return OperationResult<List<ActividadVinculacionDto>>
                    .Success("El proyecto no tiene actor externo asignado",
                        new List<ActividadVinculacionDto>());
            }

            List<ActividadVinculacion> actividades =await _actividadRepository.GetActividadesDisponiblesByActorExterno(proyecto.ActorExternoID);

            if (!actividades.Any())
            {
                return OperationResult<List<ActividadVinculacionDto>>
                    .Success(
                        "No existen actividades disponibles para este proyecto",
                        new List<ActividadVinculacionDto>()
                    );
            }

            var response = actividades
                .Select(a => a.ToActividadVinculacionDto())
                .ToList();

            return OperationResult<List<ActividadVinculacionDto>>
                .Success("Actividades disponibles obtenidas", response);
        }


        public async Task ProcesarProyectosAsync(DateTime hoy)
        {
            var proyectos = await _proyectoRepository.GetProyectosEstatusActivo();

            foreach (var proyecto in proyectos)
            {
                if (proyecto.FechaFin.HasValue &&
                    proyecto.FechaFin.Value <= hoy)
                {
                    proyecto.EstadoID = EstadosProyecto.Finalizado;
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EnviarAlertasProyectosAsync(DateTime hoy)
        {
            var proyectos = await _proyectoRepository.GetProyectosEstatusActivo();
            var correoUsuarios = await _usersRepository.GetCorreoUsuariosAlertas();

            foreach (var proyecto in proyectos)
            {
                double? diasFaltantes = null;

                if (proyecto.FechaFin.HasValue)
                    diasFaltantes = (proyecto.FechaFin.Value - hoy).TotalDays;

                if (!FuncionesService.DebeNotificar(diasFaltantes))
                    continue;

                var titulo = FuncionesService.ObtenerTitulo("Proyecto", diasFaltantes);
                var body = $@"
                                <html>
                                <body style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px;'>
                                    <div style='max-width:600px; background-color:#ffffff; padding:20px; border-radius:6px;'>
                                        <h2 style='color:#333;'>🔔 Proyecto próximo a vencer</h2>

                                        <p>Hola,</p>

                                        <p>
                                            Te informamos que el proyecto 
                                            <strong>{proyecto.TituloProyecto}</strong> tiene como fecha de finalización:
                                        </p>

                                        <p style='font-size:16px;'>
                                            📅 <strong>{proyecto.FechaFin:dd/MM/yyyy}</strong>
                                        </p>

                                        <p>
                                            Por favor, revisa el estado del proyecto y realiza las acciones correspondientes.
                                        </p>

                                        <hr />

                                        <p style='font-size:12px; color:#777;'>
                                            Sistema de Vinculación Universitaria<br/>
                                            UNPHU
                                        </p>
                                    </div>
                                </body>
                                </html>";

                foreach (var usuario in correoUsuarios)
                {
                    if (!string.IsNullOrWhiteSpace(usuario.CorreoInstitucional))
                    {
                        try
                        {
                            await _emailService.SendEmail(usuario.CorreoInstitucional, titulo, body);
                        }
                        catch
                        {
                            // log sin romper
                        }
                    }
                }
            }
        }


        private bool ProyectoEstaCompleto(ProyectoVinculacion p)
        {
            return
                !string.IsNullOrWhiteSpace(p.TituloProyecto) &&
                p.FechaInicio.HasValue &&
                p.FechaFin.HasValue &&
                p.RecintoID.HasValue;
        }

        private decimal DeterminarEstadoProyecto(ProyectoVinculacion proyecto)
        {
            if (proyecto.EstadoID == EstadosProyecto.Deshabilitado)
                return EstadosProyecto.Deshabilitado;

            if (proyecto.FechaFin.HasValue &&
                proyecto.FechaFin.Value <= DateTime.UtcNow)
                return EstadosProyecto.Finalizado;

            if (!ProyectoEstaCompleto(proyecto))
                return EstadosProyecto.Parcial;

            return EstadosProyecto.Activo;
        }
    }
}
