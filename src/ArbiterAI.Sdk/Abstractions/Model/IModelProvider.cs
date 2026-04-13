namespace ArbiterAI.Sdk.Abstractions.Model;

/// <summary>
/// Represents a model provider option that can be selected by the agent builder.
/// </summary>
public interface IModelProvider
{
    /// <summary>
    /// Gets the unique provider name used by the builder to choose the model provider.
    /// </summary>
    string Name { get; }
}
