namespace ArbiterAI.Sdk.Abstractions.Agent;

/// <summary>
/// Represents a built agent runtime that executes the agent loop.
/// </summary>
public interface IAgentRuntime
{
    /// <summary>
    /// Runs the agent loop until completion or cancellation.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the execution.</param>
    Task RunAsync(CancellationToken cancellationToken = default);
}
