
using PortalDataTask.Domain.Enums;
using PortalDataTask.Domain.ValueObject;
using PortalDataTask.Infra.CrossCutting.Services.Extensions;

namespace PortalDataTask.Application.Services;

public class CategoryService : ICategoryService
{
    public List<Status> GetAllStatus()
    {
        var statusList = Enum.GetValues(typeof(StatusEnum))
            .Cast<StatusEnum>()
            .Select(d => new Status { Code = (int)d, Name = d.ToDescription() })
            .ToList();

        return statusList;
    }
   
}
