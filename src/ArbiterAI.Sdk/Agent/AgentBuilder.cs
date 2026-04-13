using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArbiterAI.Sdk.Agent;

/// <summary>
/// Default builder implementation for configuring and creating an <see cref="IAgentRuntime"/>.
/// </summary>
public sealed class AgentBuilder : IAgentBuilder
{
    /// <summary>
    /// Gets the service collection used to configure runtime dependencies.
    /// </summary>
    public IServiceCollection Services { get; } = new ServiceCollection();

    /// <summary>
    /// Initializes a new instance of the <see cref="AgentBuilder"/> class.
    /// </summary>
    public AgentBuilder(string configPath = "appsettings.json")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(configPath);

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configPath)
            .Build();

        Services.AddSingleton<IConfiguration>(configuration);
        Services.AddSingleton<IAgentRuntime, AgentRuntime>();
    }

    /// <inheritdoc />
    public IAgentBuilder AddModelProvider(IModelProvider modelProvider)
    {
        ArgumentNullException.ThrowIfNull(modelProvider);

        Services.AddSingleton<IModelProvider>(modelProvider);
        return this;
    }

    /// <inheritdoc />
    public IAgentBuilder UseModelClient(IModelClient modelClient)
    {
        ArgumentNullException.ThrowIfNull(modelClient);

        Services.AddSingleton<IModelClient>(modelClient);
        return this;
    }

    /// <inheritdoc />
    public IAgentRuntime Build()
    {
        var serviceProvider = Services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IAgentRuntime>();
    }
}
