using PortalDataTaskApi.Filters;
using System.Diagnostics.CodeAnalysis;

namespace PortalDataTaskApi.Configuration;

[ExcludeFromCodeCoverage]
public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "Portal de Sistema de Tarefa",
                Version = "v1",
                Description = "Sistema desenvolvido para controle de tarefas para usuários que usa este portal.",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "Leandro São Leandro", Email = "lsleandro@gmail.com" }
            });

            c.OperationFilter<DataTaskInfoHeaderParameterOperationFilter>();
        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
    }
}
