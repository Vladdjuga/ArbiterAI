namespace ArbiterAI.Sdk.Abstractions.Agent;

using ArbiterAI.Sdk.Abstractions.Model;

/// <summary>
/// Represents a builder used to configure and create an <see cref="IAgentRuntime"/> instance.
/// </summary>
public interface IAgentBuilder
{
    /// <summary>
    /// Adds a model provider option that can be selected by the runtime.
    /// </summary>
    /// <param name="modelProvider">The model provider to register.</param>
    /// <returns>The current builder instance.</returns>
    IAgentBuilder AddModelProvider(IModelProvider modelProvider);

    /// <summary>
    /// Sets the model client used by the built runtime.
    /// </summary>
    /// <param name="modelClient">The model client instance.</param>
    /// <returns>The current builder instance.</returns>
    IAgentBuilder UseModelClient(IModelClient modelClient);

    /// <summary>
    /// Builds the configured agent runtime.
    /// </summary>
    /// <returns>A configured <see cref="IAgentRuntime"/> instance.</returns>
    IAgentRuntime Build();
}
