# Copilot Instructions

## General Guidelines
- Follow best practices for code readability and maintainability.
- Use clear and descriptive comments to explain complex logic at a moderate level, ensuring sufficient understanding without excessive detail.

## Code Style
- Adhere to the standard C# naming conventions.
- Use consistent indentation and spacing throughout the code.

## Project-Specific Rules
- Implement an ASP.NET-style pattern for agent construction and execution.
- Ensure that `IAgentRuntime` models the orchestrating runtime loop and includes a `RunAsync(CancellationToken)` method that returns a `Task` with no prompt input. The `RunOnceAsync` method should execute a single step, while `RunAsync` manages the full agent loop using repeated `RunOnceAsync` calls.
- Keep the API close to ASP.NET style: `Agent.CreateBuilder()` should return `IAgentBuilder`, and `Build()` should produce `IAgentRuntime`.
- The `IModelProvider` abstraction should represent provider selection in the builder, not model text generation behavior.