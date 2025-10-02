# INDT Insurance Platform (Desafio Técnico)

Solução .NET 8 com dois microserviços, arquitetura hexagonal e testes.

## Serviços
- PropostaService: cria/lista/consulta propostas e altera status (EmAnalise, Aprovada, Rejeitada)
- ContratacaoService: contrata uma proposta (apenas se Aprovada) e lista contratações

## Arquitetura
- Hexagonal (Ports & Adapters) com camadas Domain, Application, Infrastructure e Api
- Comunicação entre serviços via HTTP (Contratacao → Proposta)

## Requisitos
- .NET 8 SDK
- Docker Desktop (opcional para execução via containers)

## Rodando localmente
```bash
# build
dotnet build

# testes
dotnet test

# executar APIs
# terminal 1
cd src/PropostaService.Api && dotnet run --urls http://localhost:5201
# terminal 2 (a URL do PropostaService é lida via config)
cd src/ContratacaoService.Api && dotnet run --urls http://localhost:5202 --environment Development
```

Endpoints principais:
- PropostaService
  - POST /api/propostas
  - GET  /api/propostas
  - GET  /api/propostas/{id}
  - PATCH /api/propostas/{id}/status?status=Aprovada|EmAnalise|Rejeitada
- ContratacaoService
  - POST /api/contratacoes
  - GET  /api/contratacoes

## Docker
```bash
# subir containers
docker compose up --build
# PropostaService: http://localhost:5201/swagger
# ContratacaoService: http://localhost:5202/swagger
```

A URL do PropostaService utilizada pelo ContratacaoService é configurada via `PropostaService:BaseUrl` (ambiente `PropostaService__BaseUrl`). No compose já está ajustado para `http://proposta:8080/`.

## Banco de dados
Para simplificar, foi utilizado repositório em memória. Trocar para SQL Server/PostgreSQL exige:
- Implementar repositórios em `Infrastructure` com EF Core ou Dapper
- Adicionar migrations e connection strings
- Ajustar DI em `Program.cs`

## Diagrama (simplificado)
```
ContratacaoService.Api ──> IPropostaStatusClient ──HTTP──> PropostaService.Api
     |                                 ^
     v                                 |
 IContratacaoRepository         IPropostaRepository
 (InMemory)                     (InMemory)
```

## Melhoria sugeridas
- Repositórios com EF Core + Migrations
- Testes de integração com WebApplicationFactory
- Mensageria (bonus) para eventos de contratação

## Passo a passo (exemplo rápido)
1. Criar proposta
```bash
curl -X POST http://localhost:5201/api/propostas \
  -H "Content-Type: application/json" \
  -d '{"nomeCliente":"Ana","documentoCliente":"12345678900","premio":120.5}'
```
Anote o `id` retornado.

2. Aprovar a proposta
```bash
curl -X PATCH "http://localhost:5201/api/propostas/<ID>/status?status=Aprovada"
```

3. Contratar
```bash
curl -X POST http://localhost:5202/api/contratacoes \
  -H "Content-Type: application/json" \
  -d '{"propostaId":"<ID>"}'
```
Respostas de erro relevantes no ContratacaoService:
- 404: proposta não encontrada
- 422: proposta não aprovada
