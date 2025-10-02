using System.Collections.Concurrent;
using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;

namespace ContratacaoService.Infrastructure.Persistence;

public class InMemoryContratacaoRepository : IContratacaoRepository
{
    private readonly ConcurrentDictionary<Guid, Contratacao> _store = new();

    public Task AdicionarAsync(Contratacao contratacao, CancellationToken ct = default)
    {
        _store[contratacao.Id] = contratacao;
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Contratacao>> ListarAsync(CancellationToken ct = default)
    {
        return Task.FromResult<IEnumerable<Contratacao>>(_store.Values.ToList());
    }

    public Task<Contratacao?> ObterPorPropostaIdAsync(Guid propostaId, CancellationToken ct = default)
    {
        var item = _store.Values.FirstOrDefault(x => x.PropostaId == propostaId);
        return Task.FromResult(item);
    }
}


