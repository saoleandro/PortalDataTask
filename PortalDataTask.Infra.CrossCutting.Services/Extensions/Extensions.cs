using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace PortalDataTask.Infra.CrossCutting.Services.Extensions;

[ExcludeFromCodeCoverage]
public static class Extensions
{
    public static string ToDescription(this System.Enum data)
    {
        var field = data.GetType().GetField(data.ToString());
        var descriptionAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if(descriptionAttribute.Length > 0)
        {
            return descriptionAttribute[0].Description;
        }
        return string.Empty;
    }
}
