using PortalDataTask.Infra.CrossCutting.Services.Models;

namespace PortalDataTask.Infra.CrossCutting.Services.Services.Rabbit;

public interface IRabbitPublishService
{
    Task SendMessage(MessageSendModel messageSendModel, bool fromRec = false);
}
