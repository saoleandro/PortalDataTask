using System.Net;
using System.Text.Json.Serialization;

namespace PortalDataTask.Application.Contracts;

public class BaseResponse : BaseResponse<object>
{

}

public class BaseResponse<T>
{
    private readonly List<ErrorResponse> _errors = new List<ErrorResponse>();

    [System.Text.Json.Serialization.JsonIgnore]
    public int StatusCode { get; private set; } = 200;
    [JsonPropertyName("data")]
    public T Data { get; private set; }

    [JsonPropertyName("errors")]
    public IEnumerable<ErrorResponse> Errors => _errors;

    public void AddData(T data)
    {
        Data = data;
    }

    public void AddData(T data, HttpStatusCode statusCode)
    {
        AddData(data);
        SetStatusCode(statusCode);

    }

    public void AddError(ErrorResponse error)
    {
        _errors.Add(error);
    }

    public void AddError(ErrorResponse error, HttpStatusCode statusCode)
    {
        AddError(error);
        SetStatusCode(statusCode);
    }

    public void AddErrors(IEnumerable<ErrorResponse> errors)
    {
        _errors.AddRange(errors);
    }

    public void AddErrors(IEnumerable<ErrorResponse> errors, HttpStatusCode statusCode)
    {
        AddErrors(errors);
        SetStatusCode(statusCode);
    }

    public void SetStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = (int)statusCode;
    }

}