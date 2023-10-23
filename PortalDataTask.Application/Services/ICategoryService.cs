using PortalDataTask.Domain.ValueObject;

namespace PortalDataTask.Application.Services;

public interface ICategoryService
{
    List<Status> GetAllStatus();
}
