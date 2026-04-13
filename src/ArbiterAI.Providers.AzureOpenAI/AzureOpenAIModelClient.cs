using ArbiterAI.Sdk.Abstractions.Model;

namespace ArbiterAI.Providers.AzureOpenAI;

internal sealed class AzureOpenAIModelClient : IModelClient
{
    public static AzureOpenAIModelClient Instance { get; } = new();

    public Task<string> CompleteAsync(string prompt, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(prompt);

        return Task.FromResult($"AzureOpenAI placeholder response for: {prompt}");
    }
}
