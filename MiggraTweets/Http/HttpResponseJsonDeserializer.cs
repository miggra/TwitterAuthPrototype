using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiggraTweets.Http;

internal static class HttpResponseJsonDeserializer
{
    static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    public static async Task<T> Deserialize<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(responseContent, jsonSerializerOptions);
        if (data is null)
        {
            throw new ArgumentException($"Response not corresponds to {nameof(T)}");
        }

        return data;
    }
}
