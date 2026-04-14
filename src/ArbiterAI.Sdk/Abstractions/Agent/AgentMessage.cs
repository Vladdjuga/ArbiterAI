namespace ArbiterAI.Sdk.Abstractions.Agent;

/// <summary>
/// Represents a single message in the conversation history.
/// </summary>
public sealed class AgentMessage
{
    /// <summary>
    /// Gets the message role (user, assistant, tool, system).
    /// </summary>
    public required string Role { get; init; }

    /// <summary>
    /// Gets the message content.
    /// </summary>
    public required string Content { get; init; }

    /// <summary>
    /// Gets the timestamp when the message was created.
    /// </summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
