# LabReserve

Sistema web para reserva de laboratórios e salas.

## Stack

- .NET 10 (ASP.NET Core MVC + Razor)
- Mediator (source generator) + FluentValidation
- Dapper + SQL Server 2022
- DbUp para migrations
- Bootstrap 5, jQuery, DataTables e Select2

## Estrutura

```
src/
├── LabReserve.Domain        Entidades, enums, DTOs e contratos
├── LabReserve.Application   Casos de uso (commands, queries, handlers, validators)
├── LabReserve.Persistence   Repositórios Dapper e unidade de trabalho
├── LabReserve.Migrations    Console DbUp com os scripts SQL versionados
└── LabReserve.WebApp        Controllers, views e autenticação
```

## Pré-requisitos

- .NET SDK 10
- Docker

## Executando com Docker

```bash
cp .env.example .env
docker compose up --build
```

O serviço `migrations` cria o banco e aplica os scripts pendentes antes de o `webapp` subir. A aplicação fica em `http://localhost:8080`.

## Executando localmente

```bash
docker compose up -d sqlserver
dotnet run --project src/LabReserve.Migrations
dotnet run --project src/LabReserve.WebApp
```

A connection string local fica em `src/LabReserve.WebApp/appsettings.Development.json` e `src/LabReserve.Migrations/appsettings.json`, e pode ser sobrescrita pela variável `ConnectionStrings__Default`.

## Migrations

Os scripts ficam em `src/LabReserve.Migrations/Scripts` e são executados em ordem alfabética, uma única vez cada. O histórico fica na tabela `SchemaVersions`.

1. Crie um arquivo com o próximo número: `0004_AddRoomCapacity.sql`.
2. Escreva o SQL. Use `GO` para separar lotes.
3. Rode `dotnet run --project src/LabReserve.Migrations`.

Nunca altere um script que já foi aplicado; crie um novo.
