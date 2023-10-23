using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Application.Contracts;

public class UpdateDataTaskResponse
{
    public string? Description { get; set; }
    public DateTime? ValidateDate { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
