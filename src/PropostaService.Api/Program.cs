using PropostaService.Application.UseCases;
using PropostaService.Domain.Ports;
using PropostaService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI - Hexagonal: Ports & Adapters
builder.Services.AddSingleton<IPropostaRepository, InMemoryPropostaRepository>();
builder.Services.AddScoped<PropostaCommands>();
builder.Services.AddScoped<PropostaQueries>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
