# ArbiterAI

**A modular .NET agent runtime with pluggable model providers and tools.**

![status](https://img.shields.io/badge/status-early%20development-7c3aed)
![platform](https://img.shields.io/badge/platform-.NET%2010-0ea5e9)
![license](https://img.shields.io/badge/license-MIT-22c55e)

ArbiterAI is an SDK-first project for building governed AI agents in C#.  
The repository currently includes a working core runtime loop, tool invocation abstractions, and an Azure OpenAI provider placeholder, plus scaffolding for future packages.

## What is implemented

| Area | Status | Notes |
|---|---|---|
| Solution layout (`src/ArbiterAI.slnx`) | ✅ | Multi-project solution is in place |
| Core SDK contracts (`IAgentBuilder`, `IAgentRuntime`, model/tool abstractions) | ✅ | Public abstractions are defined |
| `AgentBuilder` DI/config wiring | ✅ | Loads `appsettings.json` + environment variant |
| `AgentRuntime` single-step and loop execution | ✅ | Supports JSON-based `final` and `tool_call` response handling |
| Tool registry + invocation pipeline (`ToolFactory`, `ToolInvoker`) | ✅ | Includes schema generation and typed `ToolCallContext` |
| Agent config model (`AgentConfig`) | ✅ | Name, description, capabilities, system instructions |
| Azure OpenAI provider package | ⚠️ Partial | Provider extension exists, model client is placeholder response |
| Example console app | ✅ | Demonstrates builder/runtime usage |

## What is not implemented yet

| Area | Status |
|---|---|
| Real OpenAI provider implementation (`ArbiterAI.Providers.OpenAI`) | ❌ Not implemented (project scaffold only) |
| File system tools (`ArbiterAI.Tools.FileSystem`) | ❌ Not implemented (project scaffold only) |
| SDK tests (`ArbiterAI.Sdk.Tests`) | ❌ No test files yet |
| Policy engine and governance middleware | ❌ Not implemented |
| Audit/event pipeline | ❌ Not implemented |
| Auth integrations (for example Entra ID) | ❌ Not implemented |
| CLI package | ❌ Not implemented |
| Dashboard/UI package | ❌ Not implemented |
| CI/release automation | ❌ Not present in repo yet |

## Repository structure

```text
src/
  ArbiterAI.slnx
  appsettings.json
  ArbiterAI.Sdk/                     # Core abstractions + builder/runtime + tool pipeline
  ArbiterAI.Providers.AzureOpenAI/   # Azure provider registration + placeholder model client
  ArbiterAI.Providers.OpenAI/        # Scaffold only
  ArbiterAI.Tools.FileSystem/        # Scaffold only
  ArbiterAI.Examples/                # Console sample
  ArbiterAI.Sdk.Tests/               # Test project scaffold
```

## Quick start

### 1. Prerequisites

- .NET SDK 10.0

### 2. Build

```bash
dotnet build src/ArbiterAI.slnx
```

### 3. Run the example

```bash
dotnet run --project src/ArbiterAI.Examples/ArbiterAI.Examples.csproj
```

## Current API snapshot

```csharp
using ArbiterAI.Sdk.Agent;
using ArbiterAI.Providers.AzureOpenAI;

var builder = new AgentBuilder();
builder.AddAzureOpenAIProvider();
builder.UseModelClient(new ExampleModelClient()); // app/runtime model client

var runtime = builder.Build();
await runtime.RunAsync();
```

## Configuration

`src/appsettings.json` contains the `agent` section currently consumed by `AgentBuilder`:

- `name`
- `description`
- `capabilities`
- `systemInstructions`

Environment-specific config files are also supported via `DOTNET_ENVIRONMENT` / `ASPNETCORE_ENVIRONMENT`.

## Development notes

- This is an **early-stage** codebase; several packages are scaffolds for upcoming work.
- The roadmap and planned milestones are tracked in [`PLAN.md`](./PLAN.md).
- Contribution guidelines are in [`CONTRIBUTING.md`](./CONTRIBUTING.md).

## License

MIT — see [`LICENSE`](./LICENSE).
