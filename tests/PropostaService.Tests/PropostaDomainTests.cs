using PropostaService.Domain.Entities;

namespace PropostaService.Tests;

public class PropostaDomainTests
{
    [Fact]
    public void CriarProposta_ComDadosValidos_DeveIniciarEmAnalise()
    {
        var p = new Proposta(Guid.Empty, "João", "12345678900", 100);
        Assert.Equal(StatusProposta.EmAnalise, p.Status);
    }
}


