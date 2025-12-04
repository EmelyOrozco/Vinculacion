using Microsoft.EntityFrameworkCore; 
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Infrastructure.Persistence; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VinculacionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VinculacionDatabase"))
);

builder.Services.AddScoped<IActorEmpresaRepository, ActorEmpresaRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
