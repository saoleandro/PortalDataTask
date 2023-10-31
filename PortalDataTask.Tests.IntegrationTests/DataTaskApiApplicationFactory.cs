using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using PortalDataTask.Domain.Interfaces.Repositories;
using PortalDataTask.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PortalDataTask.Tests.IntegrationTests.Fixtures;

namespace PortalDataTask.Tests.IntegrationTests;

public class DataTaskApiApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            services.AddScoped<IDataTaskRepository, DataTaskRepository>();

        });

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("ConnectionString:SqlServer", DbFixture.ConnectionString)
            });
        });

        base.ConfigureWebHost(builder);
    }
}