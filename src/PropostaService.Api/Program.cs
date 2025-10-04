using PropostaService.Application.UseCases;
using PropostaService.Domain.Ports;
using PropostaService.Infrastructure.Persistence;
using PropostaService.Api.Middleware;

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

// Middleware de tratamento de erros (400/404)
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
