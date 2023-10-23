using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Application.Contracts;

public class GetDataTasksByFiltersRequest
{
    public string? Description { get; set; }
    public DateTime? ValidateDate { get; set; }
    public DateTime? FinalDate { get; set; }
    public StatusEnum? Status { get; set; }
}
