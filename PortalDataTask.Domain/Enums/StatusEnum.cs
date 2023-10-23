using System.ComponentModel;

namespace PortalDataTask.Domain.Enums;

public enum StatusEnum
{
    [Description("Inativo")]
    Inactive = 0,
    [Description("Ativo")]
    Active = 1,
    [Description("Pendente")]
    Pendent = 2
}
