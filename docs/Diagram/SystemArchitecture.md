# System Architecture Diagram

## High-Level Architecture

```mermaid
flowchart TB

    Client["Client Applications<br/>React / Web / Mobile"]

    Api["TaskFlow.Api<br/>ASP.NET Core"]

    BB["TaskFlow.BuildingBlocks<br/>Cross-Cutting Concerns"]

    Users["Users Module"]
    Organizations["Organizations Module"]
    Teams["Teams Module"]
    Projects["Projects Module"]
    Tasks["Tasks Module"]
    Boards["Boards Module"]
    Comments["Comments Module"]
    Notifications["Notifications Module"]
    Reporting["Reporting Module"]

    SQL["SQL Server"]

    Client --> Api

    Api --> Users
    Api --> Organizations
    Api --> Teams
    Api --> Projects
    Api --> Tasks
    Api --> Boards
    Api --> Comments
    Api --> Notifications
    Api --> Reporting

    Users --> SQL
    Organizations --> SQL
    Teams --> SQL
    Projects --> SQL
    Tasks --> SQL
    Boards --> SQL
    Comments --> SQL
    Notifications --> SQL
    Reporting --> SQL

    BB -. Shared Services .-> Users
    BB -. Shared Services .-> Organizations
    BB -. Shared Services .-> Teams
    BB -. Shared Services .-> Projects
    BB -. Shared Services .-> Tasks
    BB -. Shared Services .-> Boards
    BB -. Shared Services .-> Comments
    BB -. Shared Services .-> Notifications
    BB -. Shared Services .-> Reporting
```

---

## Internal Module Architecture

```mermaid
flowchart TB

    Api["API Endpoint"]

    App["Application Layer<br/>Commands / Queries"]

    Domain["Domain Layer<br/>Entities / Rules"]

    Infra["Infrastructure Layer<br/>Repositories"]

    Db["SQL Server"]

    Api --> App
    App --> Domain
    App --> Infra
    Infra --> Db
```

---

## Request Processing Flow

```mermaid
sequenceDiagram

    actor User

    participant API as TaskFlow.Api
    participant App as Application Layer
    participant Domain as Domain Layer
    participant Repo as Repository
    participant DB as SQL Server

    User->>API: HTTP Request

    API->>App: Command / Query

    App->>Domain: Execute Business Rules

    App->>Repo: Data Access

    Repo->>DB: Execute Query

    DB-->>Repo: Result

    Repo-->>App: Domain Data

    App-->>API: Response DTO

    API-->>User: HTTP Response
```

---

## Cross-Cutting Architecture

```mermaid
flowchart LR

    Security["PASETO Security"]
    Localization["Localization"]
    Validation["Validation"]
    Logging["Logging"]
    Observability["OpenTelemetry"]

    Modules["Business Modules"]

    Security --> Modules
    Localization --> Modules
    Validation --> Modules
    Logging --> Modules
    Observability --> Modules
```

---

## Architectural Characteristics

### Architecture Style

* Modular Monolith
* Clean Architecture
* Domain-Driven Design
* CQRS
* Vertical Slice Architecture

### Security

* PASETO Authentication
* Refresh Tokens
* Authorization Policies

### Localization

* English
* Urdu
* Extensible Multi-Language Support

### Observability

* OpenTelemetry
* Distributed Tracing
* Metrics
* Logging

### Persistence

* SQL Server
* Entity Framework Core

### Future Evolution

The architecture is intentionally designed to allow future extraction of modules into independent microservices if required.
