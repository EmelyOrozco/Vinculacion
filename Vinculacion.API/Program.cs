using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vinculacion.API.Extentions;
using Vinculacion.Application.Dtos.ActividadVinculacionDtos.PersonaVinculacion;
using Vinculacion.Application.Dtos.ActorExterno;
using Vinculacion.Application.Dtos.ActorExternoDtos;
using Vinculacion.Application.Dtos.ProyectoVinculacionDto;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Repositories.ActividadVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.ActorExternoRepository;
using Vinculacion.Application.Interfaces.Repositories.ProyectoVinculacionRepository;
using Vinculacion.Application.Interfaces.Repositories.UsuariosSistemaRepository;
using Vinculacion.Application.Interfaces.Services.IActividadVinculacionService;
using Vinculacion.Application.Interfaces.Services.IActorExternoService;
using Vinculacion.Application.Interfaces.Services.IProyectoVinculacionService;
using Vinculacion.Application.Interfaces.Services.IUsuarioSistemaService;
using Vinculacion.Application.Services;
using Vinculacion.Application.Services.ActividadVinculacionService;
using Vinculacion.Application.Services.ActorExternoService;
using Vinculacion.Application.Services.UsuariosSistemaService;
using Vinculacion.Application.Validators.ActividadVinculacionValidator;
using Vinculacion.Application.Validators.ActorExternoValidator;
using Vinculacion.Application.Validators.ProyectoVinculacionValidator;
using Vinculacion.Domain.Entities;
using Vinculacion.Persistence;
using Vinculacion.Persistence.Context;
using Vinculacion.Persistence.Repositories;
using Vinculacion.Application.Validators.UsuariosSistemaValidator;
using Vinculacion.Application.Dtos.UsuarioSistemaDto;


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

builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IProyectoActividadRepository, ProyectoActividadRepository>();
builder.Services.AddScoped<IValidator<UpdateProyectoDto>, UpdateProyectoValidator>();
builder.Services.AddTransient<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IValidator<AddActividadesToProyectoDto>,AddActividadesToProyectoDtoValidator>();


builder.Services.AddScoped<IValidator<AddProyectoDto>, AddProyectoValidator>();


builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IValidator<UsersAddDto>, UsersValidator>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailService>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = builder.Configuration["Jwt:Issuer"],
                 ValidAudience = builder.Configuration["Jwt:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };

        
        });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vinculación API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(
        Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
