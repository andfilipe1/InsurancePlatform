namespace PropostaService.Domain.Entities;

public enum StatusProposta
{
    EmAnalise = 0,
    Aprovada = 1,
    Rejeitada = 2
}

public class Proposta
{
    public Guid Id { get; private set; }
    public string NomeCliente { get; private set; }
    public string DocumentoCliente { get; private set; }
    public decimal Premio { get; private set; }
    public StatusProposta Status { get; private set; }
    public DateTime CriadaEm { get; private set; }

    public Proposta(Guid id, string nomeCliente, string documentoCliente, decimal premio)
    {
        if (string.IsNullOrWhiteSpace(nomeCliente)) throw new ArgumentException("Nome do cliente é obrigatório.");
        if (string.IsNullOrWhiteSpace(documentoCliente)) throw new ArgumentException("Documento do cliente é obrigatório.");
        if (premio <= 0) throw new ArgumentException("Prêmio deve ser maior que zero.");

        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        NomeCliente = nomeCliente.Trim();
        DocumentoCliente = documentoCliente.Trim();
        Premio = premio;
        Status = StatusProposta.EmAnalise;
        CriadaEm = DateTime.UtcNow;
    }

    public void AlterarStatus(StatusProposta novoStatus)
    {
        Status = novoStatus;
    }
}


