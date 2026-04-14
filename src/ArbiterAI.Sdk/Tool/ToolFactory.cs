using ArbiterAI.Sdk.Abstractions.Tool;
using System.Text.Json.Nodes;

namespace ArbiterAI.Sdk.Tool;

internal sealed class ToolFactory : IToolFactory
{
    private readonly Dictionary<string, ITool> _tools;
    private readonly IReadOnlyList<JsonObject> _schemas;

    public ToolFactory(IEnumerable<ITool> tools)
    {
        ArgumentNullException.ThrowIfNull(tools);

        var list = tools as IReadOnlyList<ITool> ?? tools.ToList();

        _tools = new Dictionary<string, ITool>(list.Count, StringComparer.OrdinalIgnoreCase);
        var schemas = new List<JsonObject>(list.Count);

        foreach (var tool in list)
        {
            if (!_tools.TryAdd(tool.Name, tool))
                throw new InvalidOperationException(
                    $"Tool '{tool.Name}' is already registered. Duplicate: {tool.GetType().Name}");

            schemas.Add(BuildSchema(tool));
        }

        _schemas = schemas.AsReadOnly();
    }

    public bool TryGetTool(string toolName, out ITool? tool)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(toolName);
        return _tools.TryGetValue(toolName, out tool);
    }

    public IReadOnlyList<JsonObject> GetSchemas() => _schemas;

    private static JsonObject BuildSchema(ITool tool)
    {
        return new JsonObject
        {
            ["type"] = "function",
            ["function"] = new JsonObject
            {
                ["name"] = tool.Name,
                ["description"] = tool.Description,
                ["parameters"] = tool.GetParametersSchema(),
            },
        };
    }
}

