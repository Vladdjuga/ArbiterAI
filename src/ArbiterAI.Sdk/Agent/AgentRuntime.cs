using ArbiterAI.Sdk.Abstractions.Agent;

namespace ArbiterAI.Sdk.Agent;

/// <summary>
/// Default runtime implementation for executing the agent loop.
/// </summary>
public sealed class AgentRuntime : IAgentRuntime
{
    /// <summary>
    /// Executes a single runtime step.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the execution.</param>
    public Task RunOnceAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task RunAsync(CancellationToken cancellationToken = default)
    {
        return RunOnceAsync(cancellationToken);
    }
}
