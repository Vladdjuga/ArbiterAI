using ArbiterAI.Sdk.Abstractions.Model;

namespace ArbiterAI.Providers.AzureOpenAI;

internal sealed class AzureOpenAIModelProvider : IModelProvider
{
    public static AzureOpenAIModelProvider Instance { get; } = new();

    public string Name => "AzureOpenAI";
}
