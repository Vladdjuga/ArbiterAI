namespace ArbiterAI.Sdk.Abstractions.Tool;

/// <summary>
/// Invokes registered tools by name and manages execution, logging, and error handling.
/// </summary>
public interface IToolInvoker
{
    /// <summary>
    /// Invokes a registered tool by name with the given arguments.
    /// </summary>
    /// <param name="toolCallId">The unique tool call identifier.</param>
    /// <param name="toolName">The tool name.</param>
    /// <param name="argumentsJson">The tool arguments as a raw JSON string or JSON object.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The tool execution result.</returns>
    Task<ToolResult> InvokeAsync(
        string toolCallId,
        string toolName,
        string argumentsJson,
        CancellationToken cancellationToken = default);
}
