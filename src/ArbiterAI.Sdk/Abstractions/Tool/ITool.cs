namespace ArbiterAI.Sdk.Abstractions.Tool;

/// <summary>
/// Represents a callable agent tool such as filesystem or git operations.
/// </summary>
public interface ITool
{
    /// <summary>
    /// Gets the unique tool name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Executes the tool with the given input.
    /// </summary>
    /// <param name="input">The tool input payload.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The tool execution result payload.</returns>
    Task<string> ExecuteAsync(string input, CancellationToken cancellationToken = default);
}
