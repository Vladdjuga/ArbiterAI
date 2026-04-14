# Copilot Instructions

## General Guidelines
- Follow best practices for code readability and maintainability.
- Use clear and descriptive comments to explain complex logic at a moderate level, ensuring sufficient understanding without excessive detail.
- Introduce an `ILLMClient` abstraction to support LLM-driven command/tool calling, enhancing functionality and integration. Configure `ILLMClient` on `IAgentBuilder`, ensuring that `IAgentRuntime` method signatures remain independent of `ILLMClient`.
- Introduce an `IToolFactory` that resolves concrete tools for invocation to minimize unnecessary allocations.
- Use one class per file; do not place multiple classes in a single file.
- Ensure builder configuration loading is environment-aware (e.g., Development uses appsettings.Development.json) with smart fallback behavior. Extract environment and JSON configuration organization into a separate private method in `AgentBuilder`.
- Pass tool invocation payloads as raw JSON strings to tools, with the tools responsible for parsing their own input.
- Implement a `ToolCallContext` for typed parameter access during tool invocation, and utilize `ToolResult` for handling results.
- Design an `AgentLoop` to facilitate multi-turn LLM conversations with the capability for parallel tool invocation.
- Load `AgentConfig` from the "agent" section in JSON configuration files, allowing users to customize agent identity and instructions without code changes.

## Code Style
- Adhere to the standard C# naming conventions.
- Use consistent indentation and spacing throughout the code.

## Project-Specific Rules
- Implement an ASP.NET-style pattern for agent construction and execution.
- Ensure that `IAgentRuntime` models the orchestrating runtime loop and includes a `RunAsync(CancellationToken)` method that returns a `Task` with no prompt input. The `RunOnceAsync` method should execute a single step, while `RunAsync` manages the full agent loop using repeated `RunOnceAsync` calls.
- Keep the API close to ASP.NET style: `Agent.CreateBuilder()` should return `IAgentBuilder`, and `Build()` should produce `IAgentRuntime`.
- The `IModelProvider` abstraction should represent provider selection in the builder, not model text generation behavior.