using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;
using ArbiterAI.Sdk.Abstractions.Tool;

namespace ArbiterAI.Sdk.Agent;

/// <summary>
/// Default runtime implementation for executing the agent loop.
/// </summary>
public sealed class AgentRuntime : IAgentRuntime
{
    private readonly IReadOnlyList<IModelProvider> _modelProviders;
    private readonly IToolInvoker _toolInvoker;
    private readonly IModelClient _modelClient;
    private readonly AgentConfig _config;
    private readonly List<AgentMessage> _history;

    /// <summary>
    /// Gets the conversation history.
    /// </summary>
    public IReadOnlyList<AgentMessage> History => _history.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of the <see cref="AgentRuntime"/> class.
    /// </summary>
    /// <param name="modelProviders">The configured model providers.</param>
    /// <param name="toolInvoker">The tool invoker.</param>
    /// <param name="modelClient">The configured model client.</param>
    /// <param name="config">The agent configuration.</param>
    public AgentRuntime(
        IEnumerable<IModelProvider> modelProviders,
        IToolInvoker toolInvoker,
        IModelClient modelClient,
        AgentConfig config)
    {
        ArgumentNullException.ThrowIfNull(modelProviders);
        ArgumentNullException.ThrowIfNull(toolInvoker);
        ArgumentNullException.ThrowIfNull(modelClient);
        ArgumentNullException.ThrowIfNull(config);

        _modelProviders = modelProviders.ToList();
        _toolInvoker = toolInvoker;
        _modelClient = modelClient;
        _config = config;
        _history = [];
    }

    /// <summary>
    /// Executes a single runtime step with the given user message.
    /// </summary>
    /// <param name="userMessage">The user message to process.</param>
    /// <param name="cancellationToken">A token used to cancel the execution.</param>
    /// <returns>The model response for the current step.</returns>
    public async Task<string> RunOnceAsync(string userMessage, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userMessage);

        if (_modelProviders.Count == 0)
            throw new InvalidOperationException("No model provider has been registered.");

        // Add user message to history
        _history.Add(new AgentMessage { Role = "user", Content = userMessage });

        var modelProvider = _modelProviders[0];

        var prompt = string.Format("""
            Provider: {0}
            
            Agent Name: {1}
            Agent Description: {2}
            
            System Instructions:
            {3}
            
            Return valid JSON only.
            
            You have two response options:
            
            1. Final response (when done):
            {{
              "type": "final",
              "content": "Your response text here"
            }}
            
            2. Call a tool:
            {{
              "type": "tool_call",
              "toolCall": {{
                "toolName": "tool_name",
                "inputJson": "{\"param1\":\"value1\",\"param2\":\"value2\"}"
              }}
            }}
            
            Choose one response type above.
            
            User Message: {4}
            """, 
            modelProvider.Name,
            _config.Name,
            _config.Description,
            _config.SystemInstructions,
            userMessage);

        var response = await _modelClient.CompleteAsync(
            prompt,
            cancellationToken);

        if (!response.TryParseAgentModelResponse(out var parsedResponse) || parsedResponse is null)
            return response;

        if (string.Equals(parsedResponse.Type, "tool_call", StringComparison.OrdinalIgnoreCase))
        {
            if (parsedResponse.ToolCall is null)
                throw new InvalidOperationException("Model returned tool_call without toolCall payload.");

            var result = await _toolInvoker.InvokeAsync(
                "tool-call-step",
                parsedResponse.ToolCall.ToolName,
                parsedResponse.ToolCall.InputJson,
                cancellationToken);

            // Add tool response to history
            _history.Add(new AgentMessage { Role = "tool", Content = result.Output });

            return result.Output;
        }

        if (string.Equals(parsedResponse.Type, "final", StringComparison.OrdinalIgnoreCase))
        {
            var finalContent = parsedResponse.Content ?? string.Empty;

            // Add assistant response to history
            _history.Add(new AgentMessage { Role = "assistant", Content = finalContent });

            return finalContent;
        }

        return response;
    }

    /// <inheritdoc />
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var defaultMessage = "Hello, please help me.";
        _ = await RunOnceAsync(defaultMessage, cancellationToken);
    }
}
