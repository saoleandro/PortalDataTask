using Microsoft.Extensions.Hosting;
using PortalDataTask.Consumer.Application.Interfaces;

namespace PortalDataTask.Consumer;

public class Worker : BackgroundService
{
    private readonly IMessageConsumer _messageConsumer;

	public Worker(IMessageConsumer messageConsumer) 
	{
		_messageConsumer = messageConsumer;
	}

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Factory.StartNew(() => { _messageConsumer.Proccess(); }, stoppingToken);
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return base.StopAsync(cancellationToken);
    }
}
