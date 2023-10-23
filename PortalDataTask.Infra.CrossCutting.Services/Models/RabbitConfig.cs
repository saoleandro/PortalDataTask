namespace PortalDataTask.Infra.CrossCutting.Services.Models;

public class RabbitConfig
{
    public string? User { get; set; }
    public string? Password { get; set; }
    public int Port { get; set; }
    public string? Host { get; set; }
    public string? VirtualHost { get; set; }
    public QueueConfig? Send { get; set; }
}
