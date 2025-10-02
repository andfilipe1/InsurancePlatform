using PropostaService.Domain.Entities;

namespace PropostaService.Application.DTOs;

public record CriarPropostaRequest(string NomeCliente, string DocumentoCliente, decimal Premio);

public record PropostaResponse(Guid Id, string NomeCliente, string DocumentoCliente, decimal Premio, StatusProposta Status, DateTime CriadaEm);


