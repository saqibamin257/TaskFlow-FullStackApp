# Authentication Flow

## Overview

TaskFlow uses **PASETO (Platform-Agnostic Security Tokens)** for authentication instead of traditional JWT tokens.

PASETO provides a more secure tokenization approach by eliminating many common JWT implementation pitfalls and enforcing secure cryptographic defaults.

The authentication system supports:

* User Login
* Access Tokens
* Refresh Tokens
* Role-Based Authorization
* Policy-Based Authorization
* Secure Session Management

---

# High-Level Authentication Flow

```mermaid
flowchart TB

    User["User"]

    Login["Login Request"]

    Validation["Validate Credentials"]

    Token["Generate PASETO Access Token"]

    Refresh["Generate Refresh Token"]

    Response["Return Tokens"]

    User --> Login
    Login --> Validation
    Validation --> Token
    Token --> Refresh
    Refresh --> Response
```

---

# Login Flow

```mermaid
sequenceDiagram

    actor User

    participant API as TaskFlow.Api
    participant UserModule as Users Module
    participant DB as Database

    User->>API: Login Request

    API->>UserModule: Validate Credentials

    UserModule->>DB: Find User

    DB-->>UserModule: User Record

    UserModule->>UserModule: Verify Password

    UserModule->>UserModule: Generate PASETO Token

    UserModule->>UserModule: Generate Refresh Token

    UserModule-->>API: Authentication Result

    API-->>User: Access Token + Refresh Token
```

---

# Authenticated Request Flow

```mermaid
sequenceDiagram

    actor User

    participant API as TaskFlow.Api

    participant Auth as Authentication Middleware

    participant Module as Business Module

    User->>API: Request + Access Token

    API->>Auth: Validate PASETO Token

    Auth->>Auth: Validate Signature

    Auth->>Auth: Validate Expiration

    Auth->>Auth: Extract Claims

    Auth-->>API: Authenticated User

    API->>Module: Execute Request

    Module-->>API: Result

    API-->>User: Response
```

---

# Refresh Token Flow

```mermaid
sequenceDiagram

    actor User

    participant API as TaskFlow.Api

    participant Auth as Authentication Service

    participant DB as Database

    User->>API: Refresh Token Request

    API->>Auth: Validate Refresh Token

    Auth->>DB: Check Stored Token

    DB-->>Auth: Token Valid

    Auth->>Auth: Generate New Access Token

    Auth->>Auth: Generate New Refresh Token

    Auth-->>API: New Tokens

    API-->>User: Updated Tokens
```

---

# Authorization Flow

```mermaid
flowchart LR

    Request["Incoming Request"]

    Token["Validated PASETO Token"]

    Claims["User Claims"]

    Policy["Authorization Policy"]

    Role["Role Validation"]

    Permission["Permission Validation"]

    Resource["Protected Resource"]

    Request --> Token

    Token --> Claims

    Claims --> Policy

    Policy --> Role

    Role --> Permission

    Permission --> Resource
```

---

# Token Structure

## Access Token

Contains:

* User Id
* Email
* Roles
* Permissions
* Expiration Information

Purpose:

* Authenticate Requests
* Authorize Actions

---

## Refresh Token

Contains:

* Token Identifier
* User Reference
* Expiration Information

Purpose:

* Renew Access Tokens
* Maintain User Sessions

---

# Authentication Components

## Users Module

Responsibilities:

* Login
* User Validation
* Password Verification

---

## BuildingBlocks.Security

Responsibilities:

* PASETO Token Generation
* Token Validation
* Refresh Token Management

---

## Authentication Middleware

Responsibilities:

* Token Extraction
* Token Validation
* User Context Population

---

## Authorization Policies

Responsibilities:

* Role Validation
* Permission Validation
* Resource Protection

---

# Security Features

## PASETO Authentication

Benefits:

* Secure-by-default
* Modern cryptography
* Simpler implementation
* Protection against algorithm confusion attacks

---

## Refresh Tokens

Benefits:

* Reduced login frequency
* Better user experience
* Improved security

---

## Password Security

Features:

* Password Hashing
* Password Verification
* Secure Storage

---

# Future Enhancements

## Multi-Factor Authentication (MFA)

Features:

* Email Verification
* Authenticator Apps
* Backup Codes

---

## Social Authentication

Providers:

* Google
* Microsoft
* GitHub

---

## Session Management

Features:

* Active Sessions
* Device Tracking
* Session Revocation

---

## Security Monitoring

Features:

* Login Auditing
* Failed Login Detection
* Suspicious Activity Monitoring

```
```
