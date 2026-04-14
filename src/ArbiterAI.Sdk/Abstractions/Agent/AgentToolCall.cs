namespace ArbiterAI.Sdk.Abstractions.Agent;

/// <summary>
/// Represents a standardized request to invoke a tool.
/// </summary>
public sealed class AgentToolCall
{
    /// <summary>
    /// Gets the target tool name.
    /// </summary>
    public required string ToolName { get; init; }

    /// <summary>
    /// Gets the tool input as raw JSON string.
    /// </summary>
    public required string InputJson { get; init; }
}
