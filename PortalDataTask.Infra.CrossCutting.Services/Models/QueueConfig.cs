namespace PortalDataTask.Infra.CrossCutting.Services.Models;

public class QueueConfig
{
    public string? Exchange { get; set; }
    public string? ExchangeReproccess { get; set; }
    public string? Queue { get; set; }
    public string? QueueReproccess { get; set; }
    public string? QueueError { get; set; }
    public string? RoutingKey { get; set; }
    public int RetryInMs { get; set; }
    public long ErrorRetryInMs { get; set; }
    public int RetryAttemps { get; set; }
}
