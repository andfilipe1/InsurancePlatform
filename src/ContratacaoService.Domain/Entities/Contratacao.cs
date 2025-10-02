namespace ContratacaoService.Domain.Entities;

public class Contratacao
{
    public Guid Id { get; private set; }
    public Guid PropostaId { get; private set; }
    public DateTime DataContratacaoUtc { get; private set; }

    public Contratacao(Guid propostaId)
    {
        if (propostaId == Guid.Empty) throw new ArgumentException("PropostaId inválido");
        Id = Guid.NewGuid();
        PropostaId = propostaId;
        DataContratacaoUtc = DateTime.UtcNow;
    }
}


