using PropostaService.Application.DTOs;
using PropostaService.Domain.Entities;
using PropostaService.Domain.Ports;

namespace PropostaService.Application.UseCases;

public class PropostaCommands
{
    private readonly IPropostaRepository _repo;

    public PropostaCommands(IPropostaRepository repo)
    {
        _repo = repo;
    }

    public async Task<PropostaResponse> CriarAsync(CriarPropostaRequest request, CancellationToken ct = default)
    {
        var proposta = new Proposta(Guid.NewGuid(), request.NomeCliente, request.DocumentoCliente, request.Premio);
        await _repo.AdicionarAsync(proposta, ct);
        return Map(proposta);
    }

    public async Task AlterarStatusAsync(Guid id, StatusProposta status, CancellationToken ct = default)
    {
        var existente = await _repo.ObterPorIdAsync(id, ct) ?? throw new KeyNotFoundException("Proposta não encontrada");
        existente.AlterarStatus(status);
        await _repo.AtualizarAsync(existente, ct);
    }

    private static PropostaResponse Map(Proposta p) => new(p.Id, p.NomeCliente, p.DocumentoCliente, p.Premio, p.Status, p.CriadaEm);
}

public class PropostaQueries
{
    private readonly IPropostaRepository _repo;

    public PropostaQueries(IPropostaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<PropostaResponse>> ListarAsync(CancellationToken ct = default)
    {
        var items = await _repo.ListarAsync(ct);
        return items.Select(p => new PropostaResponse(p.Id, p.NomeCliente, p.DocumentoCliente, p.Premio, p.Status, p.CriadaEm));
    }

    public async Task<PropostaResponse?> ObterAsync(Guid id, CancellationToken ct = default)
    {
        var p = await _repo.ObterPorIdAsync(id, ct);
        return p is null ? null : new PropostaResponse(p.Id, p.NomeCliente, p.DocumentoCliente, p.Premio, p.Status, p.CriadaEm);
    }
}


