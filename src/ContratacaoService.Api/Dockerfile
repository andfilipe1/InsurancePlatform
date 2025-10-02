# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ../../INDT.InsurancePlatform.sln ./
COPY ../ContratacaoService.Domain/ ./ContratacaoService.Domain/
COPY ../ContratacaoService.Application/ ./ContratacaoService.Application/
COPY ../ContratacaoService.Infrastructure/ ./ContratacaoService.Infrastructure/
COPY ./ ./ContratacaoService.Api/
RUN dotnet restore ContratacaoService.Api/ContratacaoService.Api.csproj
RUN dotnet publish ContratacaoService.Api/ContratacaoService.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "ContratacaoService.Api.dll"]


