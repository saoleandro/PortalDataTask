using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalDataTask.Application.Services;
using PortalDataTask.Domain.Interfaces.Repositories;
using PortalDataTask.Infra.CrossCutting.Services.Models;
using PortalDataTask.Infra.CrossCutting.Services.Services.Rabbit;
using PortalDataTask.Infra.Data.Repository;

namespace PortalDataTask.Infra.CrossCutting.IoC.DI;

public static class RegisterDependencies
{
    public static void RegisterInterfaces(this IServiceCollection servicesCollection, IConfiguration configuration)
    {
        //Services
        servicesCollection.AddScoped<IDataTaskService, DataTaskService>();
        servicesCollection.AddScoped<ICategoryService, CategoryService>();
        servicesCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        servicesCollection.AddScoped<IRabbitPublishService, RabbitPublishService>();

        //Repository
        servicesCollection.AddScoped<IDataTaskRepository, DataTaskRepository>();
        servicesCollection.AddScoped<IUserRepository, UserRepository>();

        //Options
        servicesCollection.Configure<RabbitSendOptions>(configuration.GetSection("RabbitMQ"));
    }
}
