namespace PortalDataTask.Infra.CrossCutting.Services.Models;

public class RabbitSendOptions
{
    public string? User { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public string? Port { get; set; }
    public string? VirtualHost { get; set; }
    public string? Exchange { get; set; }
    public string? RoutingKey { get; set; }
}
