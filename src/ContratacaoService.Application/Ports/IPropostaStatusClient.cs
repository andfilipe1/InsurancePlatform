namespace ContratacaoService.Application.Ports;

public enum PropostaStatusDto { EmAnalise, Aprovada, Rejeitada }

public interface IPropostaStatusClient
{
    Task<PropostaStatusDto?> ObterStatusAsync(Guid propostaId, CancellationToken ct = default);
}


