using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PortalDataTask.Infra.Data;
using PortalDataTaskApi.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace PortalDataTaskApi.Configuration;

[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddApiVersioning();
        serviceCollection.AddVersioning();

        serviceCollection.AddOptions();

        serviceCollection.AddDbContext<ContextDb>(
            options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        serviceCollection.AddHealthChecks()
            .AddSqlServer(connectionString: configuration.GetConnectionString("SqlServer")!,
                          name: "SQL Server Instance")
            .AddRabbitMQ(
            new Uri($"amqp://{configuration.GetValue<string>("RabbitMQ:User")}:" +
                    $"{configuration.GetValue<string>("RabbitMQ:Password")}@" +
                    $"{configuration.GetValue<string>("RabbitMQ:Host")}:" +
                    $"{configuration.GetValue<string>("RabbitMQ:Port")}" +
                    $"{configuration.GetValue<string>("RabbitMQ:VirtualHost")}"),
            name: "RabbitMQ",
            failureStatus: HealthStatus.Degraded,
            timeout: TimeSpan.FromSeconds(1),
            tags: new string[] { "services" }            
            );
    }

    public static void UseApiConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        //app.UseHttpsRedirection();
        app.MapControllers();
        app.MapHealthChecks("/health");
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
