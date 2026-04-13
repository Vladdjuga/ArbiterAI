using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;
using ArbiterAI.Sdk.Abstractions.Tool;
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
        var configuration = BuildConfiguration(configPath);

        Services.AddSingleton<IConfiguration>(configuration);
        Services.AddSingleton<IAgentRuntime, AgentRuntime>();
    }

    private static IConfiguration BuildConfiguration(string configPath)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var directory = Path.GetDirectoryName(configPath);
        var fileName = Path.GetFileNameWithoutExtension(configPath);
        var extension = Path.GetExtension(configPath);

        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(configPath, optional: true, reloadOnChange: true);

        if (!string.IsNullOrWhiteSpace(environment))
        {
            var environmentConfigPath = Path.Combine(directory ?? string.Empty, $"{fileName}.{environment}{extension}");
            configurationBuilder.AddJsonFile(environmentConfigPath, optional: true, reloadOnChange: true);
        }

        return configurationBuilder.Build();
    }

    /// <inheritdoc />
    public IAgentBuilder AddModelProvider(IModelProvider modelProvider)
    {
        ArgumentNullException.ThrowIfNull(modelProvider);

        Services.AddSingleton<IModelProvider>(modelProvider);
        return this;
    }

    /// <inheritdoc />
    public IAgentBuilder AddTool(ITool tool)
    {
        ArgumentNullException.ThrowIfNull(tool);

        Services.AddSingleton<ITool>(tool);
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
