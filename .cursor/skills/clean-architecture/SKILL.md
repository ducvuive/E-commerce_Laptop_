---
name: clean-architecture
description: Review, design, scaffold, or refactor Clean Architecture solutions, especially .NET/C# projects with Domain, Application, Infrastructure, Persistence, Presentation, API, Contract, Dependency Injection, MediatR, repository abstractions, AssemblyReference markers, and architecture tests. Use when Codex is asked to explain layer responsibilities, validate dependency direction, add missing layers/folders, create architecture tests, or decide where code should live in a Clean Architecture codebase.
---

# Clean Architecture

## Core Rule

Keep dependencies pointing inward:

```text
API / Presentation / Infrastructure / Persistence / MessageBus
                |
                v
            Application
                |
                v
              Domain
```

Treat `Domain` as the business core. It must not depend on web, database, infrastructure, application services, DTO contracts, or framework-specific details.

## First Pass

When reviewing or changing a project:

1. Inspect the solution tree, `.sln`, `.csproj` files, namespaces, and existing tests.
2. Separate generated output (`bin`, `obj`, `.vs`) from source.
3. Identify project references and compare them with the intended dependency rules.
4. Check namespace and project naming consistency before judging architecture correctness.
5. Run tests when feasible, but do not trust green architecture tests until their rules are inspected.
6. Explain findings by layer, with concrete file references when possible.

## Layer Responsibilities

### API

Use `API` as the executable host and composition root.

It may contain:

- `Program.cs`
- middleware setup
- authentication and authorization setup
- OpenAPI/Swagger setup
- DI composition calls such as `AddApplication`, `AddPersistence`, `AddInfrastructure`, `AddPresentation`
- appsettings and environment configuration

Avoid business logic in API. Controllers/endpoints should receive requests, delegate to Presentation/Application, and return responses.

### Presentation

Use `Presentation` for HTTP-facing delivery concerns.

It may contain:

- controllers
- minimal API endpoint groups
- API versioning
- request/response mapping
- presentation-specific filters
- response conversion from `Result` to HTTP status codes

Prefer `Presentation -> Application`. Avoid `Presentation -> Infrastructure` unless the codebase intentionally accepts a looser pragmatic style. Presentation should not call `DbContext`, concrete repositories, queues, email clients, or external providers directly.

### Application

Use `Application` for use cases and orchestration.

It may contain:

- commands and queries
- MediatR handlers
- validators
- pipeline behaviors
- application services
- transaction orchestration
- interfaces for infrastructure needs
- DTOs internal to use cases, when no separate Contract project exists

Application may depend on `Domain`. It should not depend on `Persistence`, `Infrastructure`, `Presentation`, or `API`.

### Application/Abstractions

Put interfaces here when a use case needs a capability implemented by an outer layer.

Examples:

- `IEmailService`
- `ITokenProvider`
- `ICurrentUserService`
- `IDateTimeProvider`
- `IFileStorage`
- `IPaymentGateway`
- `IUnitOfWork`
- query services/read gateways when they are application-specific

Implement these interfaces in Infrastructure or Persistence. This is dependency inversion: Application owns the contract, outer layers provide details.

### Application/Behaviors

Use MediatR pipeline behaviors for cross-cutting use case concerns:

- validation
- logging
- transactions
- performance measurement
- authorization
- exception handling

Keep handlers focused on the use case. Do not repeat validation/logging/transaction code in every handler if a behavior can enforce it consistently.

### Application/DependencyInjection

Expose one extension method to register Application services:

```csharp
public static IServiceCollection AddApplication(this IServiceCollection services)
{
    services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

    return services;
}
```

API should call this extension instead of manually knowing every Application service.

### Application/UseCases

Prefer `UseCases`, not `UserCases`.

Organize by version and feature when the application is large:

```text
UseCases/
|-- V1/
|   |-- Commands/
|   |   |-- Orders/
|   |-- Queries/
|       |-- Orders/
|-- V2/
```

Commands change state. Queries read state. Keep request/handler/result names explicit.

### Domain

Use `Domain` for business concepts and rules.

It may contain:

- entities
- aggregate roots
- value objects
- domain events
- enumerations or smart enums
- domain exceptions
- domain services, only when behavior does not naturally belong to one entity/value object
- repository interfaces if the team chooses Domain-owned repository contracts

Domain must not reference Application, Persistence, Infrastructure, Presentation, API, EF Core, ASP.NET Core, MediatR, RabbitMQ, SMTP, or other technical details.

### Domain/Abstractions

Use for pure domain contracts and base concepts:

- `IEntity`
- `IAggregateRoot`
- `IDomainEvent`
- `IMessage`, if it is truly domain-level
- repository contracts for aggregates, if repository interfaces live in Domain

Avoid infrastructure-shaped abstractions here. For example, `IEmailService` usually belongs in Application, not Domain.

### Domain/Entities

Use for objects with identity and lifecycle, such as `Order`, `Customer`, `Product`.

Put business invariants inside methods on these types:

- valid state transitions
- required data rules
- aggregate consistency rules
- domain events raised by business actions

### Domain/ValueObjects

Use for immutable concepts compared by value:

- `Email`
- `Money`
- `Address`
- `Currency`

Prefer records or explicit equality. Validate invariants at construction.

### Domain/Aggregates

Use aggregate roots to protect consistency boundaries.

Only expose mutations through the aggregate root. Avoid allowing external code to mutate child entities freely.

### Persistence

Use `Persistence` for database details.

It may contain:

- `DbContext`
- EF Core entity configurations
- migrations
- repository implementations
- Unit of Work implementation
- database constants
- interceptors
- outbox tables/messages

Typical reference direction: `Persistence -> Domain`, and sometimes `Persistence -> Application` if it implements Application-owned abstractions. Avoid `Domain -> Persistence`.

### Infrastructure

Use `Infrastructure` for technical integrations outside the primary database.

It may contain:

- email providers
- token/JWT providers
- background jobs
- file storage
- payment gateways
- external API clients
- cache providers
- clock/current user implementations

Infrastructure often references `Application` to implement interfaces and may reference `Persistence` if jobs or integrations need database access. Keep this deliberate, not accidental.

### Infrastructure.MessageBus

Use a separate message bus project when broker integration is large enough.

It may contain:

- RabbitMQ/Kafka/MassTransit consumers
- integration event publishers
- command/event consumers
- retry policies
- pipe filters and observers
- mapping from external messages to Application commands

Usually reference `Application`, then call use cases through MediatR or application services.

### Contract

Use `Contract` for external boundary models:

- API DTOs shared across clients
- integration events
- gRPC proto-generated models
- common response contracts
- service-specific command/query contracts for distributed systems

Do not let Contract become a dumping ground. Business rules stay in Domain. Use cases stay in Application.

Be cautious with `Domain -> Contract`; it usually weakens the core. If a shared `Result` type is needed, decide whether it is a Domain shared kernel, an Application result, or a true Contract type.

## AssemblyReference

Use an `AssemblyReference` marker in each project when scanning assemblies:

```csharp
using System.Reflection;

namespace Demo.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```

Use it for:

- MediatR handler registration
- FluentValidation validator registration
- architecture tests
- module discovery

Prefer marker classes over string-based assembly loading. They fail at compile time when renamed incorrectly.

## Architecture Tests

Use architecture tests to protect dependency direction. NetArchTest is common in .NET.

Baseline rules:

- Domain should not depend on Application, Persistence, Infrastructure, Presentation, API, or Contract unless Contract is intentionally a shared kernel.
- Application should not depend on Persistence, Infrastructure, Presentation, or API.
- Presentation should not depend on Persistence or API; prefer not depending on Infrastructure.
- Persistence should not depend on Presentation, Infrastructure, or API.
- Infrastructure should not depend on Presentation or API.
- API may depend on all layers if it is the composition root.

Example:

```csharp
var result = Types
    .InAssembly(Domain.AssemblyReference.Assembly)
    .ShouldNot()
    .HaveDependencyOnAny(
        "Demo.Application",
        "Demo.Infrastructure",
        "Demo.Persistence",
        "Demo.Presentation",
        "Demo.API")
    .GetResult();

result.IsSuccessful.Should().BeTrue();
```

Also inspect `.csproj` references. Type-level architecture tests can pass even when a bad `ProjectReference` exists but no type from that reference is used yet.

## Review Checklist

Check these common issues:

- project/folder typo such as `Doman` instead of `Domain`
- namespaces not matching project names
- `Domain` referencing framework or infrastructure packages
- controllers containing business logic
- handlers calling concrete infrastructure classes directly
- repository implementations placed in Application or Domain
- `Presentation` calling `DbContext`
- `Application` referencing `Persistence` or `Infrastructure`
- `Contract` containing business behavior
- architecture tests missing API or project-reference checks
- `bin`, `obj`, or `.vs` committed to source

## Scaffold Guidance

For a small app, start with:

```text
src/
|-- Project.API
|-- Project.Application
|-- Project.Domain
|-- Project.Persistence
|-- Project.Infrastructure
tests/
|-- Project.Architecture.Tests
```

Add `Presentation`, `Contract`, `Infrastructure.MessageBus`, or read-model projects only when the complexity justifies them.

When creating folders, prefer capability-driven organization over empty ceremony. Do not create dozens of empty folders unless the user specifically wants a teaching scaffold.

## Response Style

When explaining Clean Architecture to a user:

- State what is correct first.
- Name the dependency rule being protected.
- Point to concrete files and references.
- Distinguish "strict Clean Architecture" from "pragmatic but acceptable".
- Give the next practical change, not only theory.

When editing code, keep changes scoped to architecture boundaries, project references, namespaces, DI registration, and tests unless the user asks for a broader refactor.
