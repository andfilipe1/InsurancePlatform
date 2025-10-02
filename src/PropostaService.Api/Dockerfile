# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ../../INDT.InsurancePlatform.sln ./
COPY ../PropostaService.Domain/ ./PropostaService.Domain/
COPY ../PropostaService.Application/ ./PropostaService.Application/
COPY ../PropostaService.Infrastructure/ ./PropostaService.Infrastructure/
COPY ./ ./PropostaService.Api/
RUN dotnet restore PropostaService.Api/PropostaService.Api.csproj
RUN dotnet publish PropostaService.Api/PropostaService.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "PropostaService.Api.dll"]


