namespace ArbiterAI.Sdk.Abstractions.Tool;

/// <summary>
/// Result of a tool execution.
/// </summary>
public sealed class ToolResult
{
    /// <summary>
    /// Gets a value indicating whether the tool execution was successful.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets the tool output (or error message if unsuccessful).
    /// </summary>
    public string Output { get; }

    private ToolResult(bool success, string output)
    {
        Success = success;
        Output = output;
    }

    /// <summary>
    /// Creates a successful tool result.
    /// </summary>
    public static ToolResult Ok(string output)
    {
        ArgumentNullException.ThrowIfNull(output);
        return new(true, output);
    }

    /// <summary>
    /// Creates a failed tool result with an error message.
    /// </summary>
    public static ToolResult Error(string message)
    {
        ArgumentNullException.ThrowIfNull(message);
        return new(false, message);
    }
}
