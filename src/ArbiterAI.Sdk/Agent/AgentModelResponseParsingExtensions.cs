using ArbiterAI.Sdk.Abstractions.Agent;
using System.Text.Json;

namespace ArbiterAI.Sdk.Agent;

internal static class AgentModelResponseParsingExtensions
{
    public static bool TryParseAgentModelResponse(this string rawResponse, out AgentModelResponse? parsedResponse)
    {
        parsedResponse = null;

        if (string.IsNullOrWhiteSpace(rawResponse))
            return false;

        try
        {
            parsedResponse = JsonSerializer.Deserialize<AgentModelResponse>(
                rawResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            return parsedResponse is not null;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
