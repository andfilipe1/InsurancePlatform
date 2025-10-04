using System.Net.Http.Json;
using ContratacaoService.Application.Ports;

namespace ContratacaoService.Infrastructure.Clients;

public class PropostaStatusHttpClient : IPropostaStatusClient
{
    private readonly HttpClient _http;

    public PropostaStatusHttpClient(HttpClient http)
    {
        _http = http;
    }

    private sealed record PropostaResponse(Guid Id, string NomeCliente, string DocumentoCliente, decimal Premio, int Status, DateTime CriadaEm);

    public async Task<PropostaStatusDto?> ObterStatusAsync(Guid propostaId, CancellationToken ct = default)
    {
        var resp = await _http.GetAsync($"api/propostas/{propostaId}", ct);
        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        resp.EnsureSuccessStatusCode();
        var dto = await resp.Content.ReadFromJsonAsync<PropostaResponse>(cancellationToken: ct);
        if (dto is null) return null;

        return dto.Status switch
        {
            0 => PropostaStatusDto.EmAnalise,
            1 => PropostaStatusDto.Aprovada,
            2 => PropostaStatusDto.Rejeitada,
            _ => null
        };
    }
}


