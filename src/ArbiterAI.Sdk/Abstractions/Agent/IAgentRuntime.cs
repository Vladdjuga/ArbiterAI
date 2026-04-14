namespace ArbiterAI.Sdk.Abstractions.Agent;

/// <summary>
/// Represents a built agent runtime that executes the agent loop.
/// </summary>
public interface IAgentRuntime
{
    /// <summary>
    /// Gets the conversation history.
    /// </summary>
    IReadOnlyList<AgentMessage> History { get; }

    /// <summary>
    /// Executes a single runtime step with the given user message.
    /// </summary>
    /// <param name="userMessage">The user message to process.</param>
    /// <param name="cancellationToken">A token used to cancel the execution.</param>
    /// <returns>The model response generated for the step.</returns>
    Task<string> RunOnceAsync(string userMessage, CancellationToken cancellationToken = default);

    /// <summary>
    /// Runs the agent loop until completion or cancellation.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the execution.</param>
    Task RunAsync(CancellationToken cancellationToken = default);
}
