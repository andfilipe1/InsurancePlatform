using System.Collections.Concurrent;
using PropostaService.Domain.Entities;
using PropostaService.Domain.Ports;

namespace PropostaService.Infrastructure.Persistence;

public class InMemoryPropostaRepository : IPropostaRepository
{
    private readonly ConcurrentDictionary<Guid, Proposta> _store = new();

    public Task AdicionarAsync(Proposta proposta, CancellationToken ct = default)
    {
        _store[proposta.Id] = proposta;
        return Task.CompletedTask;
    }

    public Task AtualizarAsync(Proposta proposta, CancellationToken ct = default)
    {
        _store[proposta.Id] = proposta;
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Proposta>> ListarAsync(CancellationToken ct = default)
    {
        return Task.FromResult<IEnumerable<Proposta>>(_store.Values.ToList());
    }

    public Task<Proposta?> ObterPorIdAsync(Guid id, CancellationToken ct = default)
    {
        _store.TryGetValue(id, out var proposta);
        return Task.FromResult(proposta);
    }
}


