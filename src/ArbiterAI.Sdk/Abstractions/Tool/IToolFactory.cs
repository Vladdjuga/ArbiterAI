using System.Text.Json.Nodes;

namespace ArbiterAI.Sdk.Abstractions.Tool;

/// <summary>
/// Resolves registered tool instances by name and provides cached tool schemas for the LLM.
/// </summary>
public interface IToolFactory
{
    /// <summary>
    /// Tries to resolve a registered tool by name.
    /// </summary>
    /// <param name="toolName">The tool name.</param>
    /// <param name="tool">The resolved tool instance when found.</param>
    /// <returns><c>true</c> when the tool exists; otherwise <c>false</c>.</returns>
    bool TryGetTool(string toolName, out ITool? tool);

    /// <summary>
    /// Gets all registered tool schemas formatted for LLM consumption.
    /// </summary>
    /// <returns>A read-only list of JSON tool schema objects.</returns>
    IReadOnlyList<JsonObject> GetSchemas();
}
