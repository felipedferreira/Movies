# Movies Project Architecture

This document explains the architecture and relationships between the projects in the Movies solution, which follows **Clean Architecture** (Hexagonal Architecture) principles.

## Architecture Overview

The solution is organized into layers that enforce separation of concerns and dependency direction. Dependencies flow inward—outer layers depend on inner layers, never the reverse.

```
┌─────────────────────────────────────────────────┐
│         Movies.WebService (Presentation)        │
│              (Web API / Entry Point)            │
└──────────────────┬──────────────────────────────┘
                   │
       ┌───────────┴───────────┐
       │                       │
┌──────▼────────────────┐ ┌───▼──────────────────────────────┐
│  Movies.Application   │ │  Movies.Persistance.Postgres    │
│   (Use Cases)         │ │    (Persistence Adapter)         │
└──────┬────────────────┘ └───┬──────────────────────────────┘
       │                       │
       └───────────┬───────────┘
                   │
         ┌─────────▼──────────┐
         │   Movies.Application│
         │   .Abstractions     │
         │  (Contracts/Ports)  │
         └─────────┬───────────┘
                   │
         ┌─────────▼──────────┐
         │  Movies.Domain     │
         │  (Business Logic)  │
         └────────────────────┘
```

## Project Descriptions

### 1. **Movies.Domain** (Foundation Layer)
**Purpose:** Core business logic and domain entities  
**Dependencies:** None  
**Responsibilities:**
- Domain entities and value objects
- Business rules and invariants
- No external dependencies (database, web frameworks, etc.)

### 2. **Movies.Application.Abstractions** (Contract Layer)
**Purpose:** Defines interfaces and contracts for the application layer  
**Dependencies:** `Movies.Domain`  
**Responsibilities:**
- Repository interfaces (ports)
- Service interfaces
- DTOs and application models
- Acts as the boundary between business logic and infrastructure

### 3. **Movies.Application** (Use Cases Layer)
**Purpose:** Implements application use cases and business workflows  
**Dependencies:** `Movies.Application.Abstractions`  
**Responsibilities:**
- Use case implementations
- Application services
- Orchestrates domain logic with persistence
- Coordinates between domain and external systems

### 4. **Movies.Persistance.Postgres** (Adapter Layer)
**Purpose:** Implements data persistence using PostgreSQL  
**Dependencies:** `Movies.Application.Abstractions`  
**Responsibilities:**
- Concrete repository implementations
- Database access and ORM integration
- Adapts PostgreSQL to the repository contracts
- Database migrations and schema management
- *Note: Listed under `Adapters/` to reflect that it's an interchangeable persistence adapter*

### 5. **Movies.WebService** (Presentation/Entry Point Layer)
**Purpose:** Web API and HTTP request handling  
**Dependencies:** `Movies.Application`, `Movies.Persistance.Postgres`  
**Responsibilities:**
- ASP.NET Core web API endpoints
- HTTP request/response handling
- Dependency injection and service configuration
- Wires together application logic and persistence implementations
- Docker containerization

## Dependency Rules

The architecture enforces these dependency directions:

| From | To | Allowed? |
|------|-----|----------|
| Domain | Anything | ❌ No (Domain has no outward dependencies) |
| Abstractions | Domain | ✅ Yes |
| Application | Abstractions | ✅ Yes |
| Adapters (Persistence) | Abstractions | ✅ Yes |
| WebService | Application, Adapters | ✅ Yes |
| WebService | Domain | ✅ Yes (transitively) |

## How It Works Together

1. **WebService** is the entry point—it handles HTTP requests and delegates to **Application**
2. **Application** implements business logic by orchestrating **Domain** entities using abstractions
3. **Application** calls repository methods defined in **Abstractions** (interfaces)
4. **Persistance.Postgres** implements those interfaces, translating repository calls to database operations
5. **Domain** contains the pure business rules that drive everything

## Building and Running

### Build the entire solution:
```bash
dotnet build
```

### Run the web service:
```bash
dotnet run --project src/Movies.WebService
```

### Docker deployment:
```bash
docker-compose up
```

## Design Benefits

- **Testability:** Business logic in Domain and Application can be tested without databases
- **Maintainability:** Changes to infrastructure (e.g., switching databases) only affect Adapters
- **Flexibility:** Easy to add new adapters (REST API, gRPC, message queues) without changing core logic
- **Clarity:** Clear layer responsibilities and data flow
