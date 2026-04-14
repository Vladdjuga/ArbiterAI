using ArbiterAI.Providers.AzureOpenAI;
using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;
using ArbiterAI.Sdk.Abstractions.Tool;
using ArbiterAI.Sdk.Agent;

// Build agent with configuration loaded from appsettings.json
var builder = new AgentBuilder();
builder.AddAzureOpenAIProvider();
builder.UseModelClient(new ExampleModelClient());

// Optionally override configuration programmatically
// builder.ConfigureAgent(cfg =>
// {
//     cfg.Name = "Custom Agent";
//     cfg.Description = "A custom agent";
// });

var runtime = builder.Build();
Console.WriteLine($"Agent: {runtime.History}");
Console.WriteLine("Running example...");
await runtime.RunAsync();

Console.WriteLine($"\nConversation History ({runtime.History.Count} messages):");
foreach (var msg in runtime.History)
{
    Console.WriteLine($"  [{msg.Role}]: {msg.Content.Substring(0, Math.Min(50, msg.Content.Length))}...");
}

internal sealed class ExampleAgentBuilder : IAgentBuilder
{
    private readonly List<IModelProvider> _providers = [];
    private readonly List<ITool> _tools = [];
    private IModelClient? _modelClient;

    public string? SelectedProviderName { get; private set; }
    public bool HasModelClient => _modelClient is not null;

    public IAgentBuilder AddModelProvider(IModelProvider modelProvider)
    {
        ArgumentNullException.ThrowIfNull(modelProvider);

        _providers.Add(modelProvider);
        SelectedProviderName ??= modelProvider.Name;
        return this;
    }

    public IAgentBuilder AddTool(ITool tool)
    {
        ArgumentNullException.ThrowIfNull(tool);

        _tools.Add(tool);
        return this;
    }

    public IAgentBuilder UseModelClient(IModelClient modelClient)
    {
        ArgumentNullException.ThrowIfNull(modelClient);

        _modelClient = modelClient;
        return this;
    }

    public IAgentRuntime Build() => new ExampleAgentRuntime();
}

internal sealed class ExampleAgentRuntime : IAgentRuntime
{
    private readonly List<AgentMessage> _history = [];

    public IReadOnlyList<AgentMessage> History => _history.AsReadOnly();

    public Task<string> RunOnceAsync(string userMessage, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example runtime single step executed with message: {userMessage}");
        _history.Add(new AgentMessage { Role = "user", Content = userMessage });
        var response = "Example response";
        _history.Add(new AgentMessage { Role = "assistant", Content = response });
        return Task.FromResult(response);
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        _ = await RunOnceAsync("Hello, help me!", cancellationToken);
    }
}

internal sealed class ExampleModelClient : IModelClient
{
    public Task<string> CompleteAsync(string prompt, CancellationToken cancellationToken = default)
    {
        return Task.FromResult($"Echo: {prompt}");
    }
}
