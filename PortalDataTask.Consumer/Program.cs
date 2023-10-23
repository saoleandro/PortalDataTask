
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortalDataTask.Consumer;
using PortalDataTask.Consumer.Extensions;
using Serilog;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        var loggerPath = configuration.GetValue<string>("LoggerBasePath");
        var template = configuration.GetValue<string>("LoggerFileTemplate");
        var shortDate = DateTime.Now.ToString("yyyy-MM-dd_HH");
        var fileName = $"{loggerPath}\\{shortDate}.og";

        Log.Logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(configuration)
                     .WriteTo.Console(outputTemplate: template)
                     .WriteTo.File(fileName, outputTemplate: template)
                     .CreateLogger();

        services
        .AddHostedService<Worker>()
        .AddCoreDependencies(hostContext.Configuration);
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
