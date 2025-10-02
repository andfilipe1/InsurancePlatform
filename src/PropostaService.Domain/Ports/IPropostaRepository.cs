using PropostaService.Domain.Entities;

namespace PropostaService.Domain.Ports;

public interface IPropostaRepository
{
    Task<Proposta?> ObterPorIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Proposta>> ListarAsync(CancellationToken ct = default);
    Task AdicionarAsync(Proposta proposta, CancellationToken ct = default);
    Task AtualizarAsync(Proposta proposta, CancellationToken ct = default);
}


