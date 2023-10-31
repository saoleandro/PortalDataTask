using System.Text.Json;

namespace PortalDataTask.Tests.IntegrationTests.Extensions;

public static class ResponseMessageExtensions
{
    public static async Task<T> ParseResponseToObject<T>(this HttpResponseMessage responseMessage)
    {
        var responseContent = await responseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
