using System.Text.Json;
using System.Text.Json.Nodes;

namespace ArbiterAI.Sdk.Abstractions.Tool;

/// <summary>
/// Context passed to a tool during execution, containing tool call metadata and typed parameter access.
/// </summary>
public sealed class ToolCallContext
{
    private readonly JsonObject _arguments;

    /// <summary>
    /// Gets the unique identifier for this tool call.
    /// </summary>
    public string ToolCallId { get; }

    /// <summary>
    /// Gets the name of the tool being invoked.
    /// </summary>
    public string ToolName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolCallContext"/> class.
    /// </summary>
    public ToolCallContext(string toolCallId, string toolName, JsonObject arguments)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(toolCallId);
        ArgumentException.ThrowIfNullOrWhiteSpace(toolName);
        ArgumentNullException.ThrowIfNull(arguments);

        ToolCallId = toolCallId;
        ToolName = toolName;
        _arguments = arguments;
    }

    /// <summary>
    /// Tries to get a parameter value with optional deserialization.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <param name="name">The parameter name.</param>
    /// <returns>The deserialized parameter value, or the default if not found.</returns>
    public T? GetParameter<T>(string name) where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (_arguments.TryGetPropertyValue(name, out var node))
            return node?.Deserialize<T>(JsonSerializerOptions.Default);

        return default;
    }

    /// <summary>
    /// Gets a required parameter value with optional deserialization.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <param name="name">The parameter name.</param>
    /// <returns>The deserialized parameter value.</returns>
    /// <exception cref="InvalidOperationException">Parameter is missing.</exception>
    public T GetRequiredParameter<T>(string name) where T : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var value = GetParameter<T>(name);
        return value ?? throw new InvalidOperationException($"Required parameter '{name}' is missing");
    }
}
