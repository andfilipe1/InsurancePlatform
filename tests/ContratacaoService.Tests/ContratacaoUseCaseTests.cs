using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Ports;
using ContratacaoService.Application.UseCases;
using ContratacaoService.Domain.Ports;
using ContratacaoService.Infrastructure.Persistence;

namespace ContratacaoService.Tests;

public class FakePropostaStatusClientAprovada : IPropostaStatusClient
{
    public Task<PropostaStatusDto?> ObterStatusAsync(Guid propostaId, CancellationToken ct = default)
        => Task.FromResult<PropostaStatusDto?>(PropostaStatusDto.Aprovada);
}

public class ContratacaoUseCaseTests
{
    [Fact]
    public async Task Contratar_DeveCriarQuandoPropostaAprovada()
    {
        IContratacaoRepository repo = new InMemoryContratacaoRepository();
        IPropostaStatusClient proposta = new FakePropostaStatusClientAprovada();
        var commands = new ContratacaoCommands(repo, proposta);

        var response = await commands.ContratarAsync(new ContratarRequest(Guid.NewGuid()));

        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.True(response.DataContratacaoUtc <= DateTime.UtcNow);
    }
}


