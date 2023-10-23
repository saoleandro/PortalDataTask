using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PortalDataTaskApi.Filters;

public class DataTaskInfoHeaderParameterOperationFilter : IOperationFilter
{
    private readonly IWebHostEnvironment _environment;

    public DataTaskInfoHeaderParameterOperationFilter(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!_environment.IsDevelopment()) return;

        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "X-DataTask-Info",
            In = ParameterLocation.Header,
            Description = "Header with dataTask info",
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("{\"dataTaskId\": \"1\", \"name\": \"Teste\", \"emailAddress\": \"teste@teste.com\" }")
            }

        });
    }
}
