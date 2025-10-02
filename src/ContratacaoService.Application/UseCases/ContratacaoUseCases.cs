using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Ports;
using ContratacaoService.Domain.Entities;
using ContratacaoService.Domain.Ports;
using ContratacaoService.Application.Errors;

namespace ContratacaoService.Application.UseCases;

public class ContratacaoCommands
{
    private readonly IContratacaoRepository _repo;
    private readonly IPropostaStatusClient _propostaClient;

    public ContratacaoCommands(IContratacaoRepository repo, IPropostaStatusClient propostaClient)
    {
        _repo = repo;
        _propostaClient = propostaClient;
    }

    public async Task<ContratacaoResponse> ContratarAsync(ContratarRequest request, CancellationToken ct = default)
    {
        var status = await _propostaClient.ObterStatusAsync(request.PropostaId, ct);
        if (status is null) throw new NotFoundException("Proposta não encontrada");
        if (status != PropostaStatusDto.Aprovada) throw new BusinessRuleException("Proposta não aprovada");

        var contratacao = new Contratacao(request.PropostaId);
        await _repo.AdicionarAsync(contratacao, ct);
        return new ContratacaoResponse(contratacao.Id, contratacao.PropostaId, contratacao.DataContratacaoUtc);
    }
}

public class ContratacaoQueries
{
    private readonly IContratacaoRepository _repo;

    public ContratacaoQueries(IContratacaoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ContratacaoResponse>> ListarAsync(CancellationToken ct = default)
    {
        var items = await _repo.ListarAsync(ct);
        return items.Select(c => new ContratacaoResponse(c.Id, c.PropostaId, c.DataContratacaoUtc));
    }
}


