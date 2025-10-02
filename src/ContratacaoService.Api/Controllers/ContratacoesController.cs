using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratacoesController : ControllerBase
{
    private readonly ContratacaoCommands _commands;
    private readonly ContratacaoQueries _queries;

    public ContratacoesController(ContratacaoCommands commands, ContratacaoQueries queries)
    {
        _commands = commands;
        _queries = queries;
    }

    [HttpPost]
    public async Task<ActionResult<ContratacaoResponse>> Contratar([FromBody] ContratarRequest request, CancellationToken ct)
    {
        var result = await _commands.ContratarAsync(request, ct);
        return CreatedAtAction(nameof(Listar), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContratacaoResponse>>> Listar(CancellationToken ct)
    {
        var items = await _queries.ListarAsync(ct);
        return Ok(items);
    }
}


