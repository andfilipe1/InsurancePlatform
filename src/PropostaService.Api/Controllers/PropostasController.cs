using Microsoft.AspNetCore.Mvc;
using PropostaService.Application.DTOs;
using PropostaService.Application.UseCases;
using PropostaService.Domain.Entities;

namespace PropostaService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropostasController : ControllerBase
{
    private readonly PropostaCommands _commands;
    private readonly PropostaQueries _queries;

    public PropostasController(PropostaCommands commands, PropostaQueries queries)
    {
        _commands = commands;
        _queries = queries;
    }

    [HttpPost]
    public async Task<ActionResult<PropostaResponse>> Criar([FromBody] CriarPropostaRequest req, CancellationToken ct)
    {
        var created = await _commands.CriarAsync(req, ct);
        return CreatedAtAction(nameof(Obter), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropostaResponse>>> Listar(CancellationToken ct)
    {
        var items = await _queries.ListarAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropostaResponse>> Obter([FromRoute] Guid id, CancellationToken ct)
    {
        var item = await _queries.ObterAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AlterarStatus([FromRoute] Guid id, [FromQuery] StatusProposta status, CancellationToken ct)
    {
        await _commands.AlterarStatusAsync(id, status, ct);
        return NoContent();
    }
}


