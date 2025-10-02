namespace ContratacaoService.Application.DTOs;

public record ContratarRequest(Guid PropostaId);
public record ContratacaoResponse(Guid Id, Guid PropostaId, DateTime DataContratacaoUtc);


