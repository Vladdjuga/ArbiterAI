namespace ArbiterAI.Sdk.Abstractions.Model;

/// <summary>
/// Represents a client capable of sending prompts to a configured language model.
/// </summary>
public interface IModelClient
{
    /// <summary>
    /// Sends a prompt to the language model and returns the generated response.
    /// </summary>
    /// <param name="prompt">The prompt to send.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The generated response text.</returns>
    Task<string> CompleteAsync(string prompt, CancellationToken cancellationToken = default);
}
