using System.Text.Json.Serialization;

namespace PortalDataTask.Application.Contracts;

public class ErrorResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}

