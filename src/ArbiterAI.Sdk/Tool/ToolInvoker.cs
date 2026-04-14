using ArbiterAI.Sdk.Abstractions.Tool;
using System.Text.Json.Nodes;

namespace ArbiterAI.Sdk.Tool;

internal sealed class ToolInvoker : IToolInvoker
{
    private readonly IToolFactory _toolFactory;

    public ToolInvoker(IToolFactory toolFactory)
    {
        ArgumentNullException.ThrowIfNull(toolFactory);
        _toolFactory = toolFactory;
    }

    public async Task<ToolResult> InvokeAsync(
        string toolCallId,
        string toolName,
        string argumentsJson,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(toolCallId);
        ArgumentException.ThrowIfNullOrWhiteSpace(toolName);
        ArgumentNullException.ThrowIfNull(argumentsJson);

        if (!_toolFactory.TryGetTool(toolName, out var tool))
            return ToolResult.Error($"Tool '{toolName}' is not registered.");

        JsonObject arguments;
        try
        {
            arguments = JsonNode.Parse(argumentsJson)?.AsObject()
                ?? throw new InvalidOperationException("Arguments must be a JSON object.");
        }
        catch (Exception ex)
        {
            return ToolResult.Error($"Invalid arguments JSON: {ex.Message}");
        }

        var context = new ToolCallContext(toolCallId, toolName, arguments);

        try
        {
            return await tool!.ExecuteAsync(context, cancellationToken);
        }
        catch (Exception ex)
        {
            return ToolResult.Error($"Tool execution failed: {ex.Message}");
        }
    }
}

