using PortalDataTask.Infra.CrossCutting.IoC.DI;
using PortalDataTaskApi.Configuration;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
             .ReadFrom.Configuration(builder.Configuration)
             .Enrich.FromLogContext()
             .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog((context, logConfig) =>
    logConfig
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

//Add services to the container
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Configuration);
builder.Services.RegisterInterfaces(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.UseSwaggerConfiguration();

app.UseCors(policy =>
              policy
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin());

app.Run();

public partial class Program { }