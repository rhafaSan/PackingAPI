# PackingServiceApi

API para gerenciamento de pedidos e empacotamento de produtos, desenvolvida em .NET 8 com Entity Framework Core e SQL Server.

---

## 🚀 Tecnologias utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server (via container Docker)
- Docker e Docker Compose
- Swagger para documentação da API

---

## 🐳 Rodando o projeto com Docker

### ✔️ Pré-requisitos

- [Docker](https://docs.docker.com/get-docker/) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado

---

### 🛠️ Passos para rodar

### 1. Clone o projeto

```bash
git clone https://github.com/seu-usuario/packing-service-api.git
cd packing-service-api
```

---

### 2. Estrutura dos arquivos necessários

No diretório do projeto você precisa ter:

- `Dockerfile`
- `docker-compose.yml`
- Código-fonte da API

---

### 3. Conteúdo do Dockerfile

```dockerfile
# Utiliza imagem oficial do .NET SDK para build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5255

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./PackingServiceApi.csproj"
RUN dotnet publish "./PackingServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PackingServiceApi.dll"]
```

---

### 4. Conteúdo do docker-compose.yml

```yaml
version: "3.8"

services:
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: packing_sql
    ports:
      - "1433:1433"
    environment:
      - SA_PASSWORD=YourStrong!Passw0rd
      - ACCEPT_EULA=Y
    volumes:
      - sqlserverdata:/var/opt/mssql

  api:
    build: .
    container_name: packing_api
    ports:
      - "5255:5255"
    depends_on:
      - db
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=PackingDb;User Id=sa;Password=YourStrong!Passw0rd;
    restart: on-failure

volumes:
  sqlserverdata:
```

---

### 5. Subindo os containers

No terminal, execute no diretório do projeto:

```bash
docker-compose up -d
```

Isso fará com que:

- O banco SQL Server suba na porta `1433`.
- A API suba na porta `5255`.

---

### 6. Aplicando migrations (Banco de Dados)

Existem duas formas:

✅ **Se sua API estiver configurada para aplicar migrations automaticamente no startup**, ela fará isso sozinha.

✅ **Manual (caso queira executar manualmente):**

- Liste os containers:

```bash
docker ps
```

- Execute o comando dentro do container da API:

```bash
docker exec -it packing_api dotnet ef database update
```

---

### 7. Acessando a API

Acesse via navegador ou ferramentas como Postman:

```
http://localhost:5255/swagger
```

Ou diretamente pelos endpoints da API.

---

### 8. Encerrando os containers

Quando quiser parar e remover os containers:

```bash
docker-compose down
```

---

### 🔄 Se fizer alterações no código

Reconstrua a imagem da API:

```bash
docker-compose build api
docker-compose up -d
```

---

## 🏗️ Connection String para ambiente local

Se quiser rodar a API localmente (sem Docker para a API, mas com o banco no container), utilize a seguinte string de conexão:

```plaintext
Server=localhost,1433;Database=PackingDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;
```

> **Importante:** Inclua `TrustServerCertificate=True;` para evitar erros de SSL ao usar SQL Server em containers.

---

## 🛑 Resolvendo erros comuns

- **Erro:** _"An exception has been raised that is likely due to a transient failure"_  
  ✅ Solução: No `Program.cs`, adicione:

```csharp
options.UseSqlServer(connectionString, sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure();
});
```

---

## 📄 Documentação

A documentação Swagger estará disponível após subir a API em:

```
http://localhost:5255/swagger
```

---

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se livre para abrir issues e pull requests.

---

## 📧 Contato

- ✉️ seuemail@exemplo.com
