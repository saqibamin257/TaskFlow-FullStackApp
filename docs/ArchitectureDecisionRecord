# Architecture Decision Records (ADR)

## Purpose

This document captures the key architectural and technical decisions made throughout the development of TaskFlow.

Unlike Coding Standards, which describe **how code should be written**, this document explains **why specific architectural decisions were made**.

These decisions serve as a reference for future development, onboarding, and technical discussions.

---

# ADR-001
## Vertical Slice Architecture

### Decision

TaskFlow uses Vertical Slice Architecture instead of a traditional layered architecture.

### Reason

- Groups related functionality together.
- Improves maintainability.
- Reduces coupling between features.
- Easier to navigate large codebases.
- Scales naturally as the application grows.

### Alternatives Considered

- Layered Architecture
- Feature Folders inside Layered Architecture

### Consequences

Pros

- Highly maintainable
- Easier feature development
- Better separation of concerns

Cons

- More folders
- Slight learning curve

---

# ADR-002
## MediatR (CQRS)

### Decision

Use MediatR for command and query handling.

### Reason

- Thin controllers.
- Clear separation between HTTP and business logic.
- Pipeline behaviors.
- Easy validation.
- Easier testing.

### Alternatives Considered

- Service layer
- Fat controllers

### Consequences

Pros

- Consistent architecture
- Easier testing
- Extensible

Cons

- More classes
- Slight overhead

---

# ADR-003
## Rich Domain Model

### Decision

Business behavior belongs inside domain entities.

### Reason

Avoid anemic domain models.

Example

Instead of

```csharp
organization.Name = ...
```

prefer

```csharp
organization.Update(...)
```

### Benefits

- Protects invariants.
- Encapsulates business rules.
- Easier maintenance.

---

# ADR-004
## Modular Monolith

### Decision

TaskFlow is implemented as a Modular Monolith.

### Reason

- Simpler deployment.
- Lower operational cost.
- Clear module boundaries.
- Easier local development.
- Can evolve into microservices later.

### Future

Modules can be extracted into independent services if required.

---

# ADR-005
## Global Exception Handling

### Decision

Use a single ExceptionHandlingMiddleware.

### Reason

Avoid repetitive try/catch blocks.

Centralize:

- Logging
- Localization
- HTTP response generation

### Consequences

Pros

- Clean handlers.
- Thin controllers.
- Consistent error responses.

---

# ADR-006
## OpenAPI First

### Decision

OpenAPI is the API contract.

### Reason

- Better documentation.
- Better frontend integration.
- SDK generation.
- API discoverability.
- Easier testing with Postman.

---

# ADR-007
## Localization

### Decision

All user-facing validation and error messages are localized.

### Reason

- Multi-language support.
- Consistent error handling.
- Future internationalization.

Languages

- English
- Urdu

---

# ADR-008
## Repository Pattern

### Decision

Repositories expose intention-revealing methods.

### Example

Instead of

```
Exists()
```

prefer

```
ExistsBySlugAsync()

ExistsBySlugExcludingOrganizationAsync()

GetAccessibleOrganizationsAsync()
```

### Reason

Repositories should express business intent rather than database operations.

---

# ADR-009
## Global Validation

### Decision

Use FluentValidation.

### Reason

- Keeps controllers clean.
- Reusable validators.
- Declarative validation.
- Automatic MediatR integration.

---

# ADR-010
## REST API Standards

### Decision

TaskFlow follows REST conventions.

### Standards

POST

→ 201 Created

PUT

→ 200 OK

DELETE

→ 204 No Content

GET

→ 200 OK

Responses are documented using OpenAPI.

---

# ADR-011
## CancellationToken

### Decision

Every asynchronous endpoint accepts CancellationToken.

### Reason

- Client disconnect support.
- Better scalability.
- Graceful cancellation.
- EF Core integration.
- ASP.NET Core best practice.

---

# ADR-012
## Thin Controllers

### Decision

Controllers should only:

- Receive HTTP requests.
- Dispatch MediatR requests.
- Return HTTP responses.

### Controllers should never contain

- Business logic
- Data access
- Authorization
- Validation

These responsibilities belong elsewhere.

---

# ADR-013
## Soft Delete

### Decision

Organizations are deactivated instead of physically deleted.

### Reason

- Preserve audit history.
- Future recovery.
- Historical reporting.
- Maintain relationships.

Future modules may implement soft delete using the same approach.

---

# Future ADRs

Future architectural decisions will be added here as TaskFlow evolves.

Examples:

- Domain Events
- Event Bus
- Background Jobs
- Caching
- Distributed Cache
- Authorization Policies
- Organization Membership
- RBAC
- Audit Logging
- File Storage
- Notification System
- AI Integration
