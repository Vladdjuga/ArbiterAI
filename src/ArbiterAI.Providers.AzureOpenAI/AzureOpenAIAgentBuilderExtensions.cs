using ArbiterAI.Sdk.Abstractions.Agent;
using ArbiterAI.Sdk.Abstractions.Model;

namespace ArbiterAI.Providers.AzureOpenAI;

/// <summary>
/// Extension methods for registering Azure OpenAI as a model provider.
/// </summary>
public static class AzureOpenAIAgentBuilderExtensions
{
    /// <summary>
    /// Adds Azure OpenAI as an available model provider option.
    /// </summary>
    /// <param name="builder">The agent builder.</param>
    /// <returns>The current builder instance.</returns>
    public static IAgentBuilder AddAzureOpenAIProvider(this IAgentBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .AddModelProvider(AzureOpenAIModelProvider.Instance)
            .UseModelClient(AzureOpenAIModelClient.Instance);
    }
}
