using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Application.Contracts;

public class CreateDataTaskRequest
{
    public string? Description { get; set; }
    public DateTime? ValidateDate { get; set; }
    public StatusEnum? Status { get; set; }
}
