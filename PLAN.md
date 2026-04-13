# ArbiterAI Development Plan

This file tracks what is already done and defines a step-by-step implementation plan for the project.

## 1. Current Status

### Done

- Repository initialized.
- Core documentation created and aligned to project name:
  - `README.md`
  - `LICENSE` (MIT)
  - `CONTRIBUTING.md`
- Product direction documented: SDK-first AI agent governance platform for .NET.

### In Progress

- Initial codebase scaffolding in `src/`.

### Not Started

- NuGet package structure and solution layout.
- Core SDK implementation (`AgentBuilder`, MCP server registration, policy pipeline).
- Built-in tool packages (FileSystem, Git).
- Entra ID auth package.
- CLI wrapper.
- Dashboard (Blazor + SignalR).
- Tests, CI, packaging, and publishing pipeline.

## 2. Step-by-Step Plan

## Step 1 - Create Solution Skeleton

Goal: establish a clean, scalable repository structure.

Tasks:

- Create solution file: `ArbiterAI.sln`.
- Create initial projects in `src/`:
  - `ArbiterAI.Sdk`
  - `ArbiterAI.Tools.FileSystem`
  - `ArbiterAI.Tools.Git`
  - `ArbiterAI.Auth.EntraId`
  - `ArbiterAI.Cli`
  - `ArbiterAI.Dashboard`
- Add test projects in `tests/`:
  - `ArbiterAI.Sdk.Tests`
  - `ArbiterAI.Policy.Tests`
- Set common project metadata (version, company, nullable, LangVersion, analyzers).

Exit criteria:

- Solution builds successfully with placeholder implementations.

## Step 2 - Define Core SDK Contracts

Goal: define stable abstractions before implementation.

Tasks:

- Define interfaces and models:
  - `IAgentBuilder`, `IAgent`, `IAgentContext`
  - `IMcpServer`, `IMcpTool`
  - `IPolicy`, `IPolicyResult`
  - `IAuditSink`
- Define execution pipeline contracts and lifecycle hooks.
- Add XML docs for public APIs.

Exit criteria:

- Contracts compile and are covered with basic unit tests for shape and registration.

## Step 3 - Implement AgentBuilder Fluent API

Goal: deliver the core SDK developer experience.

Tasks:

- Implement fluent methods:
  - `UseMcpServer<T>()`
  - `AddCustomTool<T>()`
  - `UsePolicy<T>()`
  - `UseAuditLog(...)`
  - `UseAuth(...)`
  - `Build()`
- Add validation (missing dependencies, duplicate registrations, invalid order).
- Ensure compile-time friendly API patterns and strong typing.

Exit criteria:

- End-to-end sample from README compiles and runs in tests.

## Step 4 - Implement Policy Engine

Goal: enforce governance before each tool/action execution.

Tasks:

- Build policy middleware chain.
- Add default policy result model (Allow/Deny + reason + metadata).
- Add sample policies:
  - no production access
  - deny delete outside allowed scope
- Add unit tests for policy ordering and short-circuit behavior.

Exit criteria:

- Policy decisions are deterministic and fully tested.

## Step 5 - Add Audit and Observability Foundation

Goal: make all agent actions traceable.

Tasks:

- Define canonical audit event schema.
- Implement in-memory and file audit sinks.
- Add adapter interface for Azure Monitor export.
- Correlate request id, actor, tool, target, and result.

Exit criteria:

- Every action path emits structured audit events.

## Step 6 - Build FileSystem and Git Tool Packages

Goal: provide first-party MCP tools.

Tasks:

- Implement FileSystem tool operations with path allow/deny rules.
- Implement Git tool operations with safe defaults.
- Add sandbox checks and command validation.
- Add focused tests for permission boundaries.

Exit criteria:

- Tools are usable through SDK with enforceable restrictions.

## Step 7 - Entra ID Authentication Integration

Goal: support enterprise identity and RBAC integration.

Tasks:

- Add auth extension package and configuration API.
- Map authenticated identity into agent context.
- Add role/claim checks used by policy engine.

Exit criteria:

- Authenticated sessions can drive policy and audit identity fields.

## Step 8 - CLI Wrapper

Goal: provide quick-start path over SDK.

Tasks:

- Implement commands:
  - `agent init`
  - `agent run`
  - `agent add-tool`
  - `agent policy list`
- Keep command behavior aligned with SDK capabilities.
- Add command help and examples.

Exit criteria:

- CLI executes basic scenario against sample config/project.

## Step 9 - Dashboard (Blazor + SignalR)

Goal: operational UI for governance and monitoring.

Tasks:

- Build pages for agent list, action log, policy state, cost metrics.
- Add SignalR stream for real-time events.
- Implement simple visual tool-chain view.

Exit criteria:

- Dashboard displays live events and basic governance data.

## Step 10 - Quality, CI, and Packaging

Goal: make project production-ready for open source.

Tasks:

- Add CI pipeline (build, test, lint, package).
- Add code quality gates and analyzer rules.
- Configure NuGet packaging metadata.
- Add versioning and release notes workflow.

Exit criteria:

- CI green on PRs and packages can be produced from tags.

## Step 11 - Documentation and Examples

Goal: make onboarding frictionless.

Tasks:

- Add `docs/` with architecture and package guides.
- Add runnable examples in `samples/`:
  - minimal SDK usage
  - policy customization
  - CLI flow
- Keep README aligned with actual implementation status.

Exit criteria:

- New contributors can run a sample in under 10 minutes.

## 3. Progress Tracking Template

Use this section during implementation updates:

- [ ] Step 1 - Solution Skeleton
- [ ] Step 2 - Core SDK Contracts
- [ ] Step 3 - AgentBuilder Fluent API
- [ ] Step 4 - Policy Engine
- [ ] Step 5 - Audit and Observability
- [ ] Step 6 - Tool Packages (FileSystem, Git)
- [ ] Step 7 - Entra ID Integration
- [ ] Step 8 - CLI Wrapper
- [ ] Step 9 - Dashboard
- [ ] Step 10 - Quality, CI, Packaging
- [ ] Step 11 - Documentation and Samples

## 4. Next Immediate Action

Create the .NET solution and base project structure in `src/` and `tests/`, then commit as `chore: bootstrap solution structure`.
