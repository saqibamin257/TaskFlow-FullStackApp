# Module Dependency Diagram

## Current Architecture

```mermaid
flowchart TB

    API["TaskFlow.Api"]

    BB["TaskFlow.BuildingBlocks"]

    Security["Security"]
    Localization["Localization"]
    Shared["Shared"]
    Middleware["Middleware"]

    Users["Users Module"]

    API --> Users

    API --> Security
    API --> Localization
    API --> Middleware

    Security --> Shared
    Localization --> Shared
    Middleware --> Shared

    Users --> Shared
    Users --> Security
```

---

## Users Module Internal Structure

```mermaid
flowchart TB

    Application["Users.Application"]

    Domain["Users.Domain"]

    Infrastructure["Users.Infrastructure"]

    Database["SQL Server"]

    Application --> Domain

    Infrastructure --> Domain

    Application --> Infrastructure

    Infrastructure --> Database
```

---

## Current Request Flow

```mermaid
sequenceDiagram

    actor User

    participant API as TaskFlow.Api

    participant Security as BuildingBlocks.Security

    participant Users as Users Module

    participant DB as SQL Server

    User->>API: Request

    API->>Security: Authenticate

    Security-->>API: Authenticated User

    API->>Users: Execute Command / Query

    Users->>DB: Read / Write

    DB-->>Users: Result

    Users-->>API: Response

    API-->>User: HTTP Response
```

---

# Target Architecture

The following diagram represents the planned evolution of the platform.

```mermaid
flowchart TB

    API["TaskFlow.Api"]

    BB["TaskFlow.BuildingBlocks"]

    Users["Users"]
    Organizations["Organizations"]
    Teams["Teams"]
    Projects["Projects"]
    Tasks["Tasks"]
    Boards["Boards"]
    Comments["Comments"]
    Attachments["Attachments"]
    Notifications["Notifications"]
    Audit["Audit"]
    Reporting["Reporting"]

    API --> Users
    API --> Organizations
    API --> Teams
    API --> Projects
    API --> Tasks
    API --> Boards
    API --> Comments
    API --> Attachments
    API --> Notifications
    API --> Audit
    API --> Reporting

    Users --> BB
    Organizations --> BB
    Teams --> BB
    Projects --> BB
    Tasks --> BB
    Boards --> BB
    Comments --> BB
    Attachments --> BB
    Notifications --> BB
    Audit --> BB
    Reporting --> BB
```

---

# Dependency Rules

TaskFlow follows strict dependency rules.

## Allowed

```text
Application → Domain
Application → Infrastructure Interfaces

Infrastructure → Domain

API → Modules

Modules → BuildingBlocks
```

## Not Allowed

```text
Domain → Infrastructure

Domain → API

Module → Module Direct References

BuildingBlocks → Modules
```

Modules should communicate through contracts, events, or application boundaries rather than direct coupling.

---

# Architectural Benefits

* Clear Module Boundaries
* Independent Business Domains
* High Testability
* Low Coupling
* Easy Feature Expansion
* Future Microservice Readiness
* Maintainable Codebase

```
```
