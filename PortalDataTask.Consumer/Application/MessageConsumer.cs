using Microsoft.Extensions.Logging;
using PortalDataTask.Consumer.Application.Interfaces;
using PortalDataTask.Infra.CrossCutting.Services.Models;
using PortalDataTask.Infra.CrossCutting.Services.Services.Rabbit;

namespace PortalDataTask.Consumer.Application;

public class MessageConsumer : IMessageConsumer
{
    private readonly ILogger<MessageConsumer> _logger;
    private readonly IRabbitConnectorService _rabbitConnectorService;
    private readonly RabbitConfig _rabbitConfig;

    public void Proccess()
    {
        
    }
}
