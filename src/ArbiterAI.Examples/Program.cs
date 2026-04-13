using ArbiterAI.Providers.AzureOpenAI;
using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;

var builder = new ExampleAgentBuilder();
builder.AddAzureOpenAIProvider();

var runtime = builder.Build();
Console.WriteLine($"Selected provider: {builder.SelectedProviderName ?? "None"}");
await runtime.RunAsync();

internal sealed class ExampleAgentBuilder : IAgentBuilder
{
    private readonly List<IModelProvider> _providers = [];

    public string? SelectedProviderName { get; private set; }

    public IAgentBuilder AddModelProvider(IModelProvider modelProvider)
    {
        ArgumentNullException.ThrowIfNull(modelProvider);

        _providers.Add(modelProvider);
        SelectedProviderName ??= modelProvider.Name;
        return this;
    }

    public IAgentRuntime Build() => new ExampleAgentRuntime();
}

internal sealed class ExampleAgentRuntime : IAgentRuntime
{
    public Task RunAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Example runtime executed.");
        return Task.CompletedTask;
    }
}
