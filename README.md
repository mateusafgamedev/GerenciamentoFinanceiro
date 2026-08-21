# GerenciamentoFinanceiro

Aplicação web para controle financeiro pessoal, desenvolvida em **ASP.NET MVC (.NET 8)** com **Entity Framework Core** e **SQL Server**. Permite registrar receitas e despesas, categorizá-las e filtrar as movimentações por categoria, tipo de transação e período.

## ✨ Funcionalidades

- Cadastro de movimentações financeiras (descrição, valor, data, categoria e tipo)
- Categorias pré-definidas (Alimentação, Salário, Saúde, Educação, Lazer, Imóvel)
- Tipos de transação: **Despesa** e **Lucro**
- Listagem de movimentações com filtros por:
  - Categoria
  - Tipo de transação (despesa/lucro)
  - Período (passadas, futuras ou de hoje)
- Remoção de movimentações
- Validação de formulário via Data Annotations

## 🛠️ Tecnologias

- [ASP.NET Core MVC] (.NET 8)
- [Entity Framework Core 8] (SQL Server)
- Bootstrap
- Docker / Docker Compose (SQL Server em container)

## 📁 Estrutura do projeto

```
GerenciamentoFinanceiro/
├── Controllers/         # HomeController - lógica das rotas e ações
├── Models/              # Categoria, Transacao, Financeiro, Filtros
├── Views/               # Páginas Razor (Index, AdicionarTransacao, etc.)
├── Data/                # AppDbContext (EF Core)
├── Migrations/          # Migrations do banco de dados
├── wwwroot/             # Arquivos estáticos (CSS, JS, imagens)
└── Program.cs           # Ponto de entrada da aplicação
```

## ▶️ Como executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (para subir o SQL Server) ou uma instância local do SQL Server

### 1. Clone o repositório

```bash
git clone https://github.com/mateusafgamedev/GerenciamentoFinanceiro.git
cd GerenciamentoFinanceiro
```

### 2. Suba o banco de dados com Docker

```bash
docker-compose up -d
```

Isso inicia um container SQL Server na porta `1433`.

### 3. Configure a connection string

Ajuste a string de conexão em `GerenciamentoFinanceiro/appsettings.json` (chave `DefaultConnection` ou `DataBaseConnection`) para apontar para o seu servidor/instância SQL Server.

### 4. Aplique as migrations

```bash
cd GerenciamentoFinanceiro
dotnet ef database update
```

### 5. Execute a aplicação

```bash
dotnet run
```

Acesse `https://localhost:{porta}` no navegador.

## 📌 Status

Projeto em desenvolvimento, usado como estudo prático de ASP.NET MVC, Entity Framework Core e boas práticas de validação de formulários.

## 📄 Licença

Este projeto não possui licença definida no momento.
