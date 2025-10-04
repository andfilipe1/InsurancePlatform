# Script para remover arquivos de build do Git
$files = @(
    "src/ContratacaoService.Api/bin/",
    "src/ContratacaoService.Api/obj/",
    "src/ContratacaoService.Application/bin/",
    "src/ContratacaoService.Application/obj/",
    "src/ContratacaoService.Domain/bin/",
    "src/ContratacaoService.Domain/obj/",
    "src/ContratacaoService.Infrastructure/bin/",
    "src/ContratacaoService.Infrastructure/obj/",
    "src/PropostaService.Api/bin/",
    "src/PropostaService.Api/obj/",
    "src/PropostaService.Application/bin/",
    "src/PropostaService.Application/obj/",
    "src/PropostaService.Domain/bin/",
    "src/PropostaService.Domain/obj/",
    "src/PropostaService.Infrastructure/bin/",
    "src/PropostaService.Infrastructure/obj/",
    "tests/ContratacaoService.Tests/bin/",
    "tests/ContratacaoService.Tests/obj/",
    "tests/PropostaService.Tests/bin/",
    "tests/PropostaService.Tests/obj/"
)

foreach ($file in $files) {
    if (Test-Path $file) {
        Write-Host "Removendo $file do Git..."
        git rm -r --cached $file 2>$null
    }
}

Write-Host "Arquivos de build removidos do controle do Git!"
