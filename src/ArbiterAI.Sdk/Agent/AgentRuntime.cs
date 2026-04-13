using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;

namespace ArbiterAI.Sdk.Agent;

/// <summary>
/// Default runtime implementation for executing the agent loop.
/// </summary>
public sealed class AgentRuntime : IAgentRuntime
{
    private readonly IReadOnlyList<IModelProvider> _modelProviders;
    private readonly IModelClient _modelClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AgentRuntime"/> class.
    /// </summary>
    /// <param name="modelProviders">The configured model providers.</param>
    /// <param name="modelClient">The configured model client.</param>
    public AgentRuntime(IEnumerable<IModelProvider> modelProviders, IModelClient modelClient)
    {
        ArgumentNullException.ThrowIfNull(modelProviders);
        ArgumentNullException.ThrowIfNull(modelClient);

        _modelProviders = modelProviders.ToList();
        _modelClient = modelClient;
    }

    /// <summary>
    /// Executes a single runtime step.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the execution.</param>
    /// <returns>The model response for the current step.</returns>
    public async Task<string> RunOnceAsync(CancellationToken cancellationToken = default)
    {
        if (_modelProviders.Count == 0)
            throw new InvalidOperationException("No model provider has been registered.");

        var modelProvider = _modelProviders[0];

        var prompt = $"RunOnceAsync using provider: {modelProvider.Name}";
        var response = await _modelClient.CompleteAsync(
            prompt,
            cancellationToken);

        return response;
    }

    /// <inheritdoc />
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        _ = await RunOnceAsync(cancellationToken);
    }
}
