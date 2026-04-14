namespace ArbiterAI.Sdk.Abstractions.Agent;

/// <summary>
/// Represents a standardized model response for a single agent step.
/// </summary>
public sealed class AgentModelResponse
{
    /// <summary>
    /// Gets the response type. Supported values are <c>final</c> and <c>tool_call</c>.
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    /// Gets the final response content when <see cref="Type"/> is <c>final</c>.
    /// </summary>
    public string? Content { get; init; }

    /// <summary>
    /// Gets the tool call payload when <see cref="Type"/> is <c>tool_call</c>.
    /// </summary>
    public AgentToolCall? ToolCall { get; init; }
}
