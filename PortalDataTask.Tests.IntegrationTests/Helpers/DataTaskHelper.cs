using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Tests.IntegrationTests.Helpers;

public static class DataTaskHelper
{
    public static readonly long IdActive = 1;
    public static readonly string? DescriptionActive = "description_1";
    public static readonly DateTime ValidateDateActive = new(2000, 1, 2);
    public static readonly StatusEnum StatusActive = StatusEnum.Active;
    public static readonly DateTime CreatedAtActive = new(2025, 10, 5);
    public static readonly DateTime UpdatedAtActive = new(2025, 10, 7);

    public static readonly long IdPendent = 2;
    public static readonly string? DescriptionPendent = "description_2";
    public static readonly DateTime ValidateDatePendent = new(2001, 1, 2);
    public static readonly StatusEnum StatusPendent = StatusEnum.Pendent;
    public static readonly DateTime CreatedAtPendent = new(2025, 11, 5);
    public static readonly DateTime UpdatedAtPendent = new(2025, 17, 7);

    public static readonly long IdInactive = 3;
    public static readonly string? DescriptionInactive = "description_3";
    public static readonly DateTime ValidateDateInactive = new(2002, 1, 2);
    public static readonly StatusEnum StatusInactive = StatusEnum.Inactive;
    public static readonly DateTime CreatedAtInactive = new(2025, 12, 5);
    public static readonly DateTime UpdatedAtInactive = new(2025, 12, 7);
}
