# 🚀 TaskFlow

A modern SaaS Project Management and Team Collaboration Platform built using **ASP.NET Core**, **Modular Monolith Architecture**, **Clean Architecture**, **DDD**, **CQRS**, **PASETO Authentication**, and **OpenTelemetry**.

TaskFlow is designed as a production-ready foundation for project management, team collaboration, workflow automation, reporting, and AI-powered productivity.

---

## 🎯 Vision

TaskFlow aims to become a modern enterprise work management platform that combines:

* Project Management
* Team Collaboration
* Task Tracking
* Workflow Automation
* Reporting & Analytics
* AI Productivity Tools

while maintaining a scalable and maintainable architecture.

---

## 🏗 Architecture

TaskFlow follows:

* Modular Monolith Architecture
* Clean Architecture
* Domain-Driven Design (DDD)
* CQRS
* Vertical Slice Architecture
* PASETO Authentication
* OpenTelemetry Instrumentation
* Multi-Language Support

### High-Level Architecture

```text
Client
   │
   ▼
TaskFlow API
   │
   ▼
Business Modules
   │
   ▼
SQL Server
```

### Current Solution Structure

```text
src/

├── TaskFlow.Api
│
├── TaskFlow.BuildingBlocks
│
└── TaskFlow.Modules
     │
     └── Users
          ├── Application
          ├── Domain
          └── Infrastructure
```

---

## ✨ Features

### Current Features

#### Users Module

* User Registration
* User Management
* Login
* Profile Management

#### Security

* PASETO Authentication
* Refresh Tokens
* Authorization Infrastructure

#### Localization

* English Support
* Urdu Support
* Localized Validation Messages
* Localized Error Messages

#### Developer Experience

* Swagger
* FluentValidation
* Global Exception Handling
* OpenTelemetry Foundation

---

## 📋 Planned Modules

* Organizations
* Teams
* Projects
* Tasks
* Boards
* Comments
* Attachments
* Notifications
* Audit Logs
* Reporting
* AI Assistant

---

## 🌍 Localization

TaskFlow supports multilingual experiences through a centralized localization framework.

### Current Languages

* English (en)
* Urdu (ur)

### Planned Languages

* Arabic
* Turkish
* German
* French

---

## 🔐 Security

TaskFlow uses **PASETO (Platform-Agnostic Security Tokens)** instead of traditional JWT tokens.

Benefits:

* Secure by Default
* Modern Cryptography
* Simplified Validation
* Protection Against Common JWT Pitfalls

---

## 📊 Observability

Current observability foundation includes:

* OpenTelemetry Instrumentation
* Request Tracing
* HTTP Client Instrumentation
* SQL Instrumentation

Future integrations:

* SigNoz
* Grafana
* Prometheus
* Loki
* Tempo

---

## 🛠 Technology Stack

### Backend

* ASP.NET Core
* C#
* Entity Framework Core
* SQL Server

### Architecture

* Modular Monolith
* Clean Architecture
* Domain-Driven Design
* CQRS

### Security

* PASETO
* Role-Based Authorization
* Policy-Based Authorization

### Infrastructure

* Docker
* OpenTelemetry

### Testing

* xUnit
* FluentAssertions
* Moq

---

## 📚 Documentation

Detailed documentation is available in the `/docs` folder.

### Architecture

* Architecture.md

### Modules

* Modules.md

### Roadmap

* Roadmap.md

---

## 🚦 Project Status

Current Phase:

```text
Phase 1 - MVP Foundation
```

Completed:

* Architecture Foundation
* Users Module
* Authentication
* Localization
* OpenTelemetry Foundation

In Progress:

* Organization Module
* Project Module
* Team Module

---

## 🎯 Roadmap

### MVP

* Organizations
* Projects
* Teams
* Tasks

### Collaboration

* Comments
* Attachments
* Notifications

### Enterprise

* Reporting
* Audit Logs
* Multi-Tenancy

### AI

* AI Task Assistant
* AI Project Insights
* AI Reporting

---

## 🤝 Contributing

Contributions, suggestions, and architectural discussions are welcome.

---

## 📄 License

This project is currently under active development.
