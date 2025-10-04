using ContratacaoService.Application.Ports;
using ContratacaoService.Application.UseCases;
using ContratacaoService.Infrastructure.Clients;
using ContratacaoService.Domain.Ports;
using ContratacaoService.Infrastructure.Persistence;
using ContratacaoService.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddSingleton<IContratacaoRepository, InMemoryContratacaoRepository>();
builder.Services.AddScoped<ContratacaoCommands>();
builder.Services.AddScoped<ContratacaoQueries>();

// HttpClient para PropostaService (ajuste a URL via config)
builder.Services.AddHttpClient<IPropostaStatusClient, PropostaStatusHttpClient>(client =>
{
    // Em dev, assumimos PropostaService em http://localhost:5119
    var baseUrl = builder.Configuration.GetValue<string>("PropostaService:BaseUrl") ?? "http://localhost:5119/";
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// Middleware de tratamento de erros (404/422)
app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
