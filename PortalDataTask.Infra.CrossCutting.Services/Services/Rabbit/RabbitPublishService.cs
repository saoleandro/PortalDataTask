using Microsoft.Extensions.Logging;
using PortalDataTask.Infra.CrossCutting.Services.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace PortalDataTask.Infra.CrossCutting.Services.Services.Rabbit;

public class RabbitPublishService : IRabbitPublishService
{
    private static IConnection _connection;
    private static ILogger<RabbitPublishService> _logger;
    private static RabbitSendOptions _rabbitSendOptions;

    public RabbitPublishService(
            ILogger<RabbitPublishService> logger, 
            IOptions<RabbitSendOptions> options)
    {
        _logger = logger;
        _rabbitSendOptions = options.Value;
    }

    public Task SendMessage(MessageSendModel messageSendModel, bool fromRec = false)
    {
        try
        {

            using (var connection = CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    if (!fromRec)
                    {
                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        channel.ExchangeDeclare(_rabbitSendOptions.Exchange, ExchangeType.Topic, true);

                        channel.BasicPublish(_rabbitSendOptions.Exchange, _rabbitSendOptions.RoutingKey, properties, messageSendModel.Body);
                    }
                    else
                    {
                        _logger.LogInformation($"Declarando exchange {_rabbitSendOptions.Exchange}");

                        channel.ExchangeDeclare(_rabbitSendOptions.Exchange, ExchangeType.Topic, true, false, new Dictionary<string, object>());

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        channel.BasicPublish(_rabbitSendOptions.Exchange, _rabbitSendOptions.RoutingKey, properties, messageSendModel.Body);
                    }

                    _logger.LogInformation($"Enviando para o exchange {_rabbitSendOptions.Exchange}");
                }
            }

            return Task.CompletedTask;
        }
        catch(Exception ex)
        {
            var error = $"Erro de conectar/enviar mensagem na fila do RabbitMQ. Favor verificar se o Portal Worker está rodando.";

            _logger.LogError(ex,"{Class} | {Method} | {Error} | Message: {Message} | StackTrace: {StackTrace} ", nameof(RabbitPublishService), nameof(SendMessage), error, ex.Message, ex?.StackTrace);
            throw new Exception(error);
        }
    }

    private static IConnection CreateConnection()
    {
        if (_connection != null && _connection.IsOpen)
            return _connection;

        var factory = new ConnectionFactory
        {
            Uri = new Uri($"amqp://{_rabbitSendOptions.User}:{_rabbitSendOptions.Password}@{_rabbitSendOptions.Host}:{_rabbitSendOptions.Port}{_rabbitSendOptions.VirtualHost}"),
            AutomaticRecoveryEnabled = true
        };

        _connection = factory.CreateConnection();

        return _connection;
    }
}
