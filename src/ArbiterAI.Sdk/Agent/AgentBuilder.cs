using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;
using ArbiterAI.Sdk.Abstractions.Tool;
using ArbiterAI.Sdk.Tool;
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
        
        // Load agent config from "agent" section, or use defaults
        var agentConfig = configuration.GetSection("agent").Get<AgentConfig>() ?? new AgentConfig();
        Services.AddSingleton(agentConfig);
        
        Services.AddSingleton<IToolFactory, ToolFactory>();
        Services.AddSingleton<IToolInvoker, ToolInvoker>();
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

    /// <summary>
    /// Configures the agent's identity and behavior.
    /// </summary>
    /// <param name="configure">Action to configure the agent.</param>
    /// <returns>The current builder instance.</returns>
    public IAgentBuilder ConfigureAgent(Action<AgentConfig> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        // Get existing config or create new
        var serviceProvider = Services.BuildServiceProvider();
        var existingConfig = serviceProvider.GetService<AgentConfig>() ?? new AgentConfig();

        // Apply configuration
        configure(existingConfig);

        // Replace in services by removing all existing registrations and adding the new one
        var descriptorsToRemove = Services.Where(d => d.ServiceType == typeof(AgentConfig)).ToList();
        foreach (var descriptor in descriptorsToRemove)
        {
            Services.Remove(descriptor);
        }

        Services.AddSingleton(existingConfig);

        return this;
    }

    /// <inheritdoc />
    public IAgentRuntime Build()
    {
        var serviceProvider = Services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IAgentRuntime>();
    }
}
