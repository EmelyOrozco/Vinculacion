using Microsoft.EntityFrameworkCore;
using Vinculacion.API.Extentions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Application.Interfaces.Services;
using Vinculacion.Application.Services;
using Vinculacion.Persistence.Context;
using Vinculacion.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VinculacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VinculacionDatabase"))
);

builder.Services.AddScoped<IActorEmpresaRepository, ActorEmpresaRepository>();
builder.Services.AddTransient<IActorEmpresaService, ActorEmpresaService>();

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
