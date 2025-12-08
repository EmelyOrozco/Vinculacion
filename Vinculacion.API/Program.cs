using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Vinculacion.API.Extentions;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;
using Vinculacion.Application.Services.ActividadVinculacionService;
using Vinculacion.Application.Services.ActorExternoService;
using Vinculacion.Application.Validators.ActividadVinculacionValidator;
using Vinculacion.Application.Validators.ActorExternoValidator;
using Vinculacion.Persistence;
using Vinculacion.Persistence.Context;
using Vinculacion.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VinculacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IActorEmpresaRepository, ActorEmpresaRepository>();
builder.Services.AddTransient<IActorEmpresaService, ActorEmpresaService>();

builder.Services.AddScoped<IActorPersonaRepository, ActorPersonaRepository>();
builder.Services.AddTransient<IActorPersonaService, ActorPersonaService>();

builder.Services.AddScoped<IPersonaVinculacionRepository, PersonaVinculacionRepository>();
builder.Services.AddTransient<IPersonaVinculacionService, PersonaVinculacionService>();

builder.Services.AddScoped<IActividadVinculacionRepository, ActividadVinculacionRepository>();
builder.Services.AddTransient<IActividadVinculacionService, ActividadVinculacionService>();

builder.Services.AddScoped<IActividadSubtareasRepository, ActividadSubtareasRepository>();
builder.Services.AddTransient<IActividadSubtareasService, ActividadSubtareasService>();

builder.Services.AddScoped<IActorExternoRepository, ActorExternoRepository>();
builder.Services.AddScoped<IPaisRepository, PaisRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IValidator<AddActorPersonaDto>, CrearActorPersonaValidator>();

builder.Services.AddScoped<IActorEmpresaClasificacionRepository, ActorEmpresaClasificacionRepository>();
builder.Services.AddScoped<IValidator<AddActorEmpresaDto>,AddActorEmpresaDtoValidator>();
builder.Services.AddScoped<IValidator<PersonaVinculacionDto>,PersonaVinculacionValidator>();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
