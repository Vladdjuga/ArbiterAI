namespace ArbiterAI.Sdk.Abstractions.Agent;

/// <summary>
/// Agent system configuration containing identity, behavior, and capabilities description.
/// </summary>
public sealed class AgentConfig
{
    /// <summary>
    /// Gets the agent name.
    /// </summary>
    public string Name { get; init; } = "Agent";

    /// <summary>
    /// Gets the agent description and behavior instructions.
    /// </summary>
    public string Description { get; init; } = "You are a helpful assistant.";

    /// <summary>
    /// Gets the list of capabilities or system instructions.
    /// </summary>
    public IReadOnlyList<string> Capabilities { get; init; } = [];

    /// <summary>
    /// Gets additional system-level instructions or constraints.
    /// </summary>
    public string SystemInstructions { get; init; } = string.Empty;
}
