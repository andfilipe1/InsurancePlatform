namespace PropostaService.Application.Ports;

public interface IClock
{
    DateTime UtcNow { get; }
}


