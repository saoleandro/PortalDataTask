using System.Diagnostics.CodeAnalysis;

namespace PortalDataTaskApi.Configuration;

[ExcludeFromCodeCoverage]
public static class VersioningConfig
{
    public static void AddVersioning(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddApiVersioning();
        servicesCollection.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

}
