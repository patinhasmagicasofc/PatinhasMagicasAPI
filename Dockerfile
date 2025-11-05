# ================================
# Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do container
WORKDIR /src

# Copia a solução e os projetos
COPY PatinhasMagicasAPI.sln ./
COPY PatinhasMagicasAPI/*.csproj ./SimuQuestAPI/

# Restaura dependências
RUN dotnet restore

# Copia todo o código do projeto
COPY PatinhasMagicasAPI/. ./PatinhasMagicasAPI/

# Build em Release
WORKDIR /src/PatinhasMagicasAPI
RUN dotnet build -c Release -o /app/build

# ================================
# Publish stage
# ================================
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# ================================
# Runtime stage
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

# Expondo a porta padrão
EXPOSE 5000

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "PatinhasMagicasAPI.dll"]
