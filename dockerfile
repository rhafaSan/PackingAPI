# Imagem base do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto
COPY . ./

# Restaurar dependências
RUN dotnet restore

# Build
RUN dotnet publish -c Release -o out

# -----------------------

# Runtime com suporte a Globalização
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# ✅ Corrige o problema de Globalization
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Instala dependências de localização
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        locales \
    && locale-gen en_US.UTF-8 \
    && locale-gen pt_BR.UTF-8 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Copia o build publicado
COPY --from=build /app/out .

# Executa
ENTRYPOINT ["dotnet", "PackingServiceApi.dll"]
