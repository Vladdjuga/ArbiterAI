using System.Text.Json.Nodes;

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
    /// Gets the human-readable tool description for the LLM.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Executes the tool with the given call context.
    /// </summary>
    /// <param name="context">The tool call context containing metadata and typed parameter access.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The tool execution result.</returns>
    Task<ToolResult> ExecuteAsync(ToolCallContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the JSON schema describing the tool's input parameters.
    /// </summary>
    /// <returns>The parameter schema as a JSON object.</returns>
    JsonObject GetParametersSchema();
}
