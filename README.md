# ArbiterAI - AI Agent Governance Platform

![Status](https://img.shields.io/badge/status-concept-orange)
![License](https://img.shields.io/badge/license-MIT-green)
![.NET](https://img.shields.io/badge/.NET-8%2B-512BD4?logo=dotnet&logoColor=white)
![MCP](https://img.shields.io/badge/MCP-supported-blue)
![Contributions](https://img.shields.io/badge/contributions-welcome-brightgreen)

> SDK-first governance for enterprise AI agents: secure, typed, auditable.

ArbiterAI is an SDK-first platform for building, configuring, and governing AI agents with MCP servers in enterprise .NET environments.

The core product is a typed C# SDK distributed via NuGet. CLI and Web UI are optional layers built on top of the same SDK.

## Overview

ArbiterAI focuses on **agent governance**, not just orchestration.

- Build agent capabilities through a fluent C# API
- Configure MCP servers as typed services
- Register custom tools and policy middleware
- Enforce guardrails before each agent action
- Integrate audit, identity, and cost tracking from day one

## Why SDK-First

- Existing CLI agent tools already cover command-based workflows
- Enterprises run on .NET and need native integration into existing systems
- Compile-time safety catches config errors before runtime
- Testability: unit-test agent configuration, policies, and tool chains
- Market gap: Semantic Kernel solves LLM orchestration, not full agent governance

## Core SDK

```csharp
var agent = new AgentBuilder()
 .UseMcpServer<FileSystemServer>(opts => opts.AllowRead("/src").DenyDelete())
 .UseMcpServer<GitServer>()
 .AddCustomTool<MyDeployTool>()
 .UsePolicy<NoProductionAccessPolicy>()
 .UseAuditLog(sink => sink.ToAzureMonitor())
 .UseAuth(auth => auth.AzureEntraId("tenant-id"))
 .Build();
```

### SDK Capabilities

- Fluent API in an ASP.NET Core middleware-pipeline style
- Typed MCP server registration and configuration
- Custom MCP tool authoring via regular C# classes
- Policy middleware chain evaluated before every action
- Programmatic file operations (README/config/files) without manual editing

## CLI Layer

The CLI is an optional quick-start interface for teams that do not need deep customization immediately.

Example commands:

- `agent init`
- `agent run`
- `agent add-tool`
- `agent policy list`

All commands map to the same underlying SDK primitives.

## Web UI Layer

A web dashboard for operational visibility and management:

- Agent lifecycle management
- Action logs and real-time monitoring
- Visual MCP tool-chain constructor
- Usage, cost, and activity analytics

## Features

- **RBAC**: define who can grant which permissions to which agents (Azure Entra ID integration)
- **Audit trail**: log every agent action, requester identity, and modified artifacts
- **Policy engine**: implement guardrails in C# classes or configuration
- **Cost tracking**: monitor token spend per user/team/department (Azure OpenAI billing)
- **Multi-tenancy**: isolate agents, policies, and data across business units
- **Sandboxing**: restrict execution environment and validate actions before execution

## Package Architecture

| Package | Purpose |
| --- | --- |
| AgentPlatform.Sdk | Core builder, MCP integration, policy engine |
| AgentPlatform.Tools.FileSystem | Built-in filesystem MCP tool |
| AgentPlatform.Tools.Git | Built-in Git MCP tool |
| AgentPlatform.Auth.EntraId | Azure Entra ID integration |
| AgentPlatform.Dashboard | Blazor dashboard as embedded middleware |
| agent-cli (dotnet tool) | SDK-powered command-line wrapper |

## Technology Stack

- C# / .NET
- ASP.NET Core
- MCP protocol
- LLM providers (Azure OpenAI-first)
- Azure Entra ID
- Blazor
- SignalR (real-time updates)
- OS API integration
- SQL / Cosmos DB
- NuGet packaging ecosystem

## Use Cases

ArbiterAI is designed for teams that need:

- strongly typed agent configuration
- operational control and policy enforcement
- identity-aware authorization
- auditable, secure, and scalable agent operations

## Getting Started (Planned)

1. Install the core package from NuGet.
2. Create an agent with AgentBuilder and register MCP servers.
3. Add policies, audit sinks, and authentication providers.
4. Run via your host application or the CLI wrapper.

## Roadmap

- [ ] Publish core SDK package
- [ ] Publish built-in FileSystem and Git MCP tools
- [ ] Publish Entra ID authentication integration
- [ ] Release CLI wrapper
- [ ] Release dashboard (Blazor + SignalR)

## Status

Early concept and architecture definition.

## Contributing

Contributions are welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) for contribution workflow, branch naming, commit style, and pull request checklist.

1. Open an issue to discuss a feature or bug.
2. Submit a pull request with a focused change.
3. Include tests or usage examples when applicable.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
