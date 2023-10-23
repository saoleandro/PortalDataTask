using PortalDataTask.Application.Contracts;
using System.Text.Json;

namespace PortalDataTaskApi.Extensions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next;
        _logger = logger;   
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment env)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex, env);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
    {
        Guid guid = Guid.NewGuid();
        _logger.LogError(ex, "An internal server error has ocurred {exceptionId}", guid);
        string message = ((!env.IsDevelopment()) ? "An internal server error has occurred" : ("Message: " + ex.Message + "\nStacktrace: " + ex.StackTrace));
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new List<ErrorResponse>
        {
            new ErrorResponse
            {
                Message = message
            }
        }));
    }
}
