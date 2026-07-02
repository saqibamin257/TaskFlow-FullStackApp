# TaskFlow Coding Standards

## Purpose

This document defines the coding standards and architectural conventions followed throughout the TaskFlow solution.

The goal is to ensure that every feature is implemented consistently, remains easy to maintain, and follows modern ASP.NET Core and Clean Architecture best practices.

---

# Solution Architecture

TaskFlow follows:

- Vertical Slice Architecture
- Feature-based organization
- MediatR (CQRS)
- Rich Domain Model
- Clean Architecture principles
- OpenAPI-first API design

Every feature should be self-contained.

---

# Project Structure

```
src
│
├── TaskFlow.Api
├── TaskFlow.BuildingBlocks
└── TaskFlow.Modules
```

Each module contains:

```
Module
│
├── Application
│   ├── Abstraction
│   ├── Features
│   └── DependencyInjection.cs
│
├── Domain
│
└── Infrastructure
```

---

# Feature Structure

Every feature should follow the same folder layout.

```
CreateUser
│
├── CreateUserCommand.cs
├── CreateUserCommandValidator.cs
├── CreateUserHandler.cs
└── CreateUserResponse.cs
```

Examples:

- CreateUser
- UpdateOrganization
- DeleteProject
- GetTasks

---

# Controllers

Controllers must remain thin.

Controllers are responsible only for:

- Receiving HTTP requests
- Basic HTTP validation
- Dispatching MediatR requests
- Returning HTTP responses

Controllers must NOT contain:

- Business logic
- Data access
- Validation logic
- Authorization logic

Example

```csharp
var result = await _mediator.Send(request, cancellationToken);

return Ok(result);
```

---

# Controller Standards

Every controller should:

- Use `[ApiController]`
- Use `[Authorize]` when authentication is required
- Accept `CancellationToken`
- Use RESTful routes
- Use route constraints (`{id:guid}`)
- Document responses using `ProducesResponseType`

Example

```csharp
[HttpPut("{id:guid}")]
```

---

# REST Response Standards

| Operation | 200 | 201 | 204 | 400 | 401 | 403 | 404 | 409 |
|-----------|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| GET Collection | ✅ | | | | ✅ | | | |
| GET By Id | ✅ | | | | ✅ | | ✅ | |
| POST | | ✅ | | ✅ | ✅ | | | *(Future)* |
| PUT | ✅ | | | ✅ | ✅ | ✅ | ✅ | *(Future)* |
| DELETE | | | ✅ | ✅ | ✅ | ✅ | ✅ | |

---

# Handlers

Handlers contain application/business orchestration.

Typical flow:

1. Retrieve entity
2. Validate existence
3. Validate authorization
4. Validate business rules
5. Execute domain behavior
6. Persist changes
7. Return response

Handlers should NOT contain:

- HTTP logic
- Logging
- try/catch
- Response generation

---

# Domain Entities

Entities are responsible for protecting their own state.

Avoid:

```csharp
organization.Name = request.Name;
```

Prefer:

```csharp
organization.UpdateName(request.Name);
```

Every business operation should have an explicit method.

Examples:

- Activate()
- Deactivate()
- UpdateName()
- UpdateSlug()
- ChangePassword()
- AssignRole()

Avoid public setters.

---

# Validation

Validation uses FluentValidation.

Validation should only validate incoming requests.

Examples:

- Required fields
- Length
- Format
- Regex
- Simple rules

Business validation belongs inside handlers or domain entities.

---

# Repository Guidelines

Repositories expose intention-revealing methods.

Avoid generic names.

Instead of:

```
Exists()
```

Prefer:

```
ExistsByEmailAsync()

ExistsBySlugAsync()

ExistsBySlugExcludingOrganizationAsync()

GetAccessibleOrganizationsAsync()
```

Read-only queries should use:

```csharp
AsNoTracking()
```

---

# Exception Handling

Exception handling is centralized.

Controllers:

- Never use try/catch

Handlers:

- Never use try/catch unless recovering or translating exceptions

Repositories:

- Never swallow exceptions

Global middleware handles:

- Logging
- Exception mapping
- Localization
- HTTP responses

---

# Logging

Logging is performed only in cross-cutting concerns.

Avoid logging inside handlers unless additional business context is required.

---

# Localization

Never hard-code validation or error messages.

Use:

- ValidationKeys
- ErrorKeys

Every key must exist in:

- en.json
- ur.json

---

# Naming Conventions

Controllers

```
UserController
OrganizationController
```

Commands

```
CreateUserCommand
UpdateOrganizationCommand
```

Queries

```
GetUsersQuery
GetOrganizationsQuery
```

Handlers

```
CreateUserHandler
UpdateOrganizationHandler
```

Responses

```
CreateUserResponse
UpdateOrganizationResponse
```

Validators

```
CreateUserCommandValidator
```

Repositories

```
IUserRepository
OrganizationRepository
```

---

# Async Guidelines

Always use async methods.

Every async endpoint should accept:

```csharp
CancellationToken cancellationToken
```

Pass the token through MediatR, repositories and EF Core.

---

# OpenAPI

Every endpoint should document realistic responses using:

```csharp
ProducesResponseType
```

The OpenAPI specification is treated as the API contract.

---

# General Principles

- Keep controllers thin.
- Keep handlers focused.
- Rich domain model over anemic entities.
- Prefer explicit methods over generic CRUD.
- Favor readability over cleverness.
- Keep business logic out of controllers.
- Keep cross-cutting concerns centralized.
- Write code for the next developer.