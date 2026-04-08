# ================================
# Build stage
# ================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Define o diret�rio de trabalho dentro do container
WORKDIR /src

# Copia a solu��o e os projetos
COPY PatinhasMagicasAPI.sln ./
COPY PatinhasMagicasAPI/*.csproj ./PatinhasMagicasAPI/

# Restaura depend�ncias
RUN dotnet restore

# Copia todo o c�digo do projeto
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
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

# Expondo a porta padr�o
EXPOSE 5000

# Comando para rodar a aplica��o
ENTRYPOINT ["dotnet", "PatinhasMagicasAPI.dll"]
