# ValidataTask

**ValidataTask** is a sample application demonstrating Domain-Driven Design (DDD) principles, built with .NET 8.0. It manages Products, Customers, and Orders using a clean architecture approach and modern .NET libraries.

## 📦 Architecture & Project Structure

The solution (`ValidataTask.sln`) is organized into the following layers:

```
/ValidataTask.sln
│
├── src/
│   ├── Core/           # Domain and common abstractions (Entities, Value Objects, Interfaces)
│   ├── Application/    # Application layer: CQRS commands & queries, handlers, DTOs, FluentValidation
│   ├── Infrastructure/ # EF Core persistence, SQLite database provider, repository implementations
│   └── Presentation/   # ASP.NET Core Web API (Controllers, middleware)
│       └── WebApi/
│
└── tests/UnitTests/    # xUnit unit tests for commands, queries, and validation pipeline
```

### Key Concepts

- **Domain-Driven Design (DDD):** Entities, Value Objects, Repositories, and domain logic clearly separated.
- **CQRS with MediatR:** Commands and Queries are segregated; each request flows through a pipeline.
- **FluentValidation:** Declarative validation rules for commands and DTOs in the Application layer.
- **Entity Framework Core + SQLite:** The Infrastructure layer uses EF Core with SQLite provider for persistence.
- **Unit Testing:** Comprehensive unit tests using xUnit and Moq for handlers, validators, and repositories.

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- (Optional) [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/)

### Clone the Repository

```bash
git clone https://github.com/yourusername/ValidataTask.git
cd ValidataTask
```

### Build the Solution

```bash
dotnet restore
dotnet build
```

### Run the Web API

From the root of the solution:

```bash
cd src/Presentation/WebApi
dotnet run
```

The API will start on `https://localhost:7152` (or the port configured in `launchSettings.json`).

### Database Migrations

EF Core migrations are configured for SQLite.

## 🛠 Usage

The API exposes endpoints to manage the following resources:

- **Products** (CRUD operations)
- **Customers** (CRUD operations)
- **Orders** (CRUD operations, including nested OrderItems)

Refer to the Swagger/OpenAPI definition at `https://localhost:7152/swagger` (or see configured port under launchSettings.json in WebApi project) for full details on available endpoints and request/response schemas.

## ✅ Running Unit Tests

From the solution root, run:

```bash
dotnet test tests/UnitTests
```

This will execute all xUnit tests covering command/query handlers and validators.
