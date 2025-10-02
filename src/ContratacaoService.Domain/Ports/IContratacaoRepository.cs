using ContratacaoService.Domain.Entities;

namespace ContratacaoService.Domain.Ports;

public interface IContratacaoRepository
{
    Task AdicionarAsync(Contratacao contratacao, CancellationToken ct = default);
    Task<IEnumerable<Contratacao>> ListarAsync(CancellationToken ct = default);
    Task<Contratacao?> ObterPorPropostaIdAsync(Guid propostaId, CancellationToken ct = default);
}


