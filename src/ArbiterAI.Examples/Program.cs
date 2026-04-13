using ArbiterAI.Providers.AzureOpenAI;
using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;
using ArbiterAI.Sdk.Abstractions.Tool;

var builder = new ExampleAgentBuilder();
builder.AddAzureOpenAIProvider();
builder.UseModelClient(new ExampleModelClient());

var runtime = builder.Build();
Console.WriteLine($"Selected provider: {builder.SelectedProviderName ?? "None"}");
Console.WriteLine($"Model client configured: {builder.HasModelClient}");
await runtime.RunAsync();

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
    public Task<string> RunOnceAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Example runtime single step executed.");
        return Task.FromResult("Example response");
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        _ = await RunOnceAsync(cancellationToken);
    }
}

internal sealed class ExampleModelClient : IModelClient
{
    public Task<string> CompleteAsync(string prompt, CancellationToken cancellationToken = default)
    {
        return Task.FromResult($"Echo: {prompt}");
    }
}
