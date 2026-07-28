# Authentication

> This document explains the architecture and design decisions behind TaskFlow's authentication system. It focuses on **why** the system is designed this way rather than documenting individual implementation details.

---

# Overview

TaskFlow uses **token-based authentication** to secure all communication between the frontend and the ASP.NET Core backend.

The authentication module is designed around the following goals:

- Maintainability
- Separation of Concerns
- Extensibility
- Single Responsibility
- Future-proof architecture

The current implementation supports:

- Login
- Logout
- Access Token Storage
- Remember Me
- Automatic Authorization Header Injection
- Centralized Authentication Error Handling

The architecture has already been prepared for future features such as:

- Refresh Tokens
- Automatic Token Refresh
- Protected Routes
- Current User Context
- Role & Permission Based Authorization

---

# Authentication Architecture

```text
                           Login Form
                               │
                               ▼
                      Authentication Service
                               │
                               ▼
                        ASP.NET Core API
                               │
                               ▼
                         Access Token
                               │
                               ▼
                          Auth Storage
                               │
             ┌─────────────────┴─────────────────┐
             │                                   │
             ▼                                   ▼
    Request Interceptor               Response Interceptor
             │                                   │
             ▼                                   ▼
      Protected API                 Authentication Recovery
```

Every component owns **one responsibility**.

---

# Authentication Flow

```text
+----------------+
|   Login Form   |
+----------------+
        |
        v
+----------------------+
| AuthenticationService|
+----------------------+
        |
        v
+----------------------+
| ASP.NET Core API     |
+----------------------+
        |
        v
+----------------------+
| Access Token         |
+----------------------+
        |
        v
+----------------------+
| Auth Storage         |
+----------------------+
        |
        v
+----------------------+
| Request Interceptor  |
+----------------------+
        |
        v
+----------------------+
| Protected API        |
+----------------------+
```

---

# Authentication Components

## 1. Login Form

**Location**

```
src/features/authentication/components
```

### Responsibility

The Login Form is responsible for user interaction.

Responsibilities include:

- Collect credentials
- Validate input using React Hook Form + Zod
- Call Authentication Service
- Display validation errors
- Display server errors
- Redirect after successful login

The Login Form **does not communicate with the API directly**.

Instead, it delegates authentication to the Authentication Service.

---

## 2. Authentication Service

**Location**

```
src/features/authentication/api/auth.service.ts
```

### Responsibility

Acts as the gateway between the UI and the Authentication API.

Current methods:

```ts
login()

logout()

refreshAccessToken() // Placeholder
```

Responsibilities:

- Call Authentication APIs
- Store Access Token
- Remove Access Token
- Expose authentication operations

The Authentication Service intentionally does **not** know anything about:

- React Components
- Forms
- UI State
- Validation
- Browser Storage implementation

---

## 3. Auth Storage

**Location**

```
src/features/authentication/storage/auth.storage.ts
```

### Responsibility

Responsible only for browser storage.

Current methods:

```ts
storeAccessToken()

getAccessToken()

removeAccessToken()
```

Depending on the **Remember Me** option, the storage implementation chooses:

- Local Storage
- Session Storage

### Why a separate storage layer?

Without AuthStorage:

- Every service would directly access Local Storage.
- Storage logic would be duplicated.
- Changing storage strategy would require updating multiple files.

Instead, browser storage is abstracted behind a single service.

---

## 4. Axios Request Interceptor

**Location**

```
src/lib/axios.ts
```

### Purpose

Automatically attach the current Access Token to every protected API request.

Without a Request Interceptor, every service would manually write:

```http
Authorization: Bearer <AccessToken>
```

This would duplicate authentication logic throughout the application.

### Request Flow

```text
Service

↓

Axios Request Interceptor

↓

Read Access Token

↓

Attach Authorization Header

↓

ASP.NET Core API
```

### Public Endpoints

Public endpoints bypass authentication using:

```ts
skipAuth: true
```

Examples:

- Login
- Refresh Token
- Forgot Password

### Similarity to ASP.NET Core

The Request Interceptor behaves similarly to ASP.NET Core Authentication Middleware.

Instead of every controller attaching authentication manually, the middleware performs the responsibility once.

---

## 5. Axios Response Interceptor

**Location**

```
src/lib/axios.ts
```

### Purpose

The Response Interceptor centralizes authentication recovery.

Every response returned by the server passes through this interceptor before reaching the calling service.

Without a Response Interceptor every API service would implement its own authentication handling.

---

### Current Responsibilities

- Detect Unauthorized (401)
- Ignore public endpoints
- Logout the user
- Redirect to Login

Current flow:

```text
API

↓

401 Unauthorized

↓

Response Interceptor

↓

Logout

↓

Redirect Login
```

---

### Future Responsibilities

Once Refresh Tokens are implemented, the Response Interceptor will automatically recover expired sessions.

Future flow:

```text
Protected API

↓

401 Unauthorized

↓

Response Interceptor

↓

Refresh Access Token

↓

Retry Original Request

↓

Return Response
```

If refresh fails:

```text
Protected API

↓

401 Unauthorized

↓

Refresh Token

↓

Failed

↓

Logout

↓

Redirect Login
```

---

### Why use a Response Interceptor?

Without a centralized interceptor every service would need:

```text
try
{
    Call API
}
catch (401)
{
    Logout
}
```

This duplicates authentication logic throughout the application.

Instead, authentication recovery is implemented once.

---

### Authentication Recovery (Planned)

The architecture is already prepared for automatic session recovery.

Once Refresh Token APIs become available, the Response Interceptor will:

1. Detect Unauthorized responses.
2. Start a Refresh Token request.
3. Ensure only one refresh request executes at a time.
4. Retry all failed requests.
5. Logout if refresh fails.

---

### Synchronization

The interceptor uses a shared:

```ts
refreshPromise
```

to guarantee only one Refresh Token request is executed.

Example:

```text
Projects API --------401

Tasks API -----------401

Users API -----------401

↓

Only one Refresh request

↓

All requests wait

↓

Retry Original Requests
```

This prevents:

- Duplicate refresh requests
- Race conditions
- Unnecessary server load

---

### Similarity to ASP.NET Core

The Response Interceptor behaves similarly to ASP.NET Core Middleware.

```text
ASP.NET Core

Authentication Middleware

↓

Controller

↓

Exception Middleware

==============================

React

Request Interceptor

↓

API

↓

Response Interceptor

↓

React Components
```

Instead of every service handling authentication failures, the Response Interceptor performs the responsibility once.

---

# Folder Structure

```
src
└── features
    └── authentication
        ├── api
        │     auth.service.ts
        │
        ├── components
        │
        ├── storage
        │     auth.storage.ts
        │
        └── types
              auth.types.ts
```

---

# Design Principles

The authentication module follows several software engineering principles.

---

## Single Responsibility Principle

Each component owns exactly one responsibility.

| Component | Responsibility |
|------------|----------------|
| Login Form | Collect credentials |
| Authentication Service | Authentication operations |
| Auth Storage | Browser storage |
| Request Interceptor | Attach Access Token |
| Response Interceptor | Recover from authentication failures |

---

## Separation of Concerns

Authentication responsibilities are divided into independent layers.

Benefits:

- Easier maintenance
- Better readability
- Easier testing
- Better scalability

---

## Single Source of Truth

The Access Token exists in only one place:

```
Auth Storage
```

Every component retrieves the token from there.

---

## Future Extensibility

The authentication architecture has been intentionally designed to support future enhancements with minimal code changes.

Planned features:

- Refresh Tokens
- Automatic Token Refresh
- Protected Routes
- Current User Context
- Multi-Tenant Authorization
- Role-Based Authorization

---

# Current Status

| Feature | Status |
|----------|--------|
| Login | ✅ |
| Logout | ✅ |
| Access Token Storage | ✅ |
| Remember Me | ✅ |
| Request Interceptor | ✅ |
| Response Interceptor | ✅ |
| Refresh Token Architecture | ✅ Designed |
| Refresh Token Backend API | ⏳ Planned |
| Auth Provider | ⏳ Planned |
| Protected Routes | ⏳ Planned |

---

# Key Takeaways

- Components never communicate with browser storage directly.
- Authentication APIs are encapsulated inside the Authentication Service.
- Browser storage is encapsulated inside Auth Storage.
- Authentication headers are added automatically by the Request Interceptor.
- Authentication failures are handled centrally by the Response Interceptor.
- The architecture is designed to evolve without breaking existing application code.

---

# Conclusion

The authentication module follows a layered architecture that emphasizes maintainability, separation of concerns, and future extensibility.

Although the current implementation only supports Access Tokens, the architecture has already been prepared for Refresh Tokens and automatic session recovery. Future authentication enhancements can be introduced with minimal changes to the existing codebase.