using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalDataTask.Infra.CrossCutting.Services.Models;
using PortalDataTask.Infra.CrossCutting.Services.Services.Rabbit;

namespace PortalDataTask.Consumer.Extensions;

public static class MiddlewareDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRabbitConnectorService, RabbitConnectorService>();

        services.AddSingleton(x =>
        {
            return new RabbitConfig
            {
                User = configuration.GetSection("RabbitMQ:User").Value,
                Password = configuration.GetSection("RabbitMQ:Password").Value,
                Port = Convert.ToInt32(configuration.GetSection("RabbitMQ:Port").Value),
                Host = configuration.GetSection("RabbitMQ:Host").Value,
                VirtualHost = configuration.GetSection("RabbitMQ:VirtualHost").Value,
                Send = new QueueConfig
                {
                    Queue = configuration.GetSection("RabbitMQ:QueueConfig:Send:Queue").Value,
                    QueueError = configuration.GetSection("RabbitMQ:QueueConfig:Send:QueueError").Value,
                    QueueReproccess = configuration.GetSection("RabbitMQ:QueueConfig:Send:QueueReprocess").Value,
                    Exchange = configuration.GetSection("RabbitMQ:QueueConfig:Send:Exchange").Value,
                    ExchangeReproccess = configuration.GetSection("RabbitMQ:QueueConfig:Send:ExchangeReproccess").Value,
                    RetryInMs = Convert.ToInt32(configuration.GetSection("RabbitMQ:QueueConfig:Send:RetryInMs").Value),
                    ErrorRetryInMs = Convert.ToInt32(configuration.GetSection("RabbitMQ:QueueConfig:Send:ErrorRetryInMs").Value),
                    RetryAttemps = Convert.ToInt32(configuration.GetSection("RabbitMQ:QueueConfig:Send:RetryAttemps").Value),
                    RoutingKey = configuration.GetSection("RabbitMQ:QueueConfig:Send:RoutingKey").Value
                }
            };
        });

        return services;
    }

}
