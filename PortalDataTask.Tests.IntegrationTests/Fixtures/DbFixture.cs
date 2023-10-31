using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using PortalDataTask.Infra.Data;
using PortalDataTask.Tests.IntegrationTests.Helpers;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace PortalDataTask.Tests.IntegrationTests.Fixtures;

public class DbFixture : IDisposable
{
    public static string ConnectionString => GetConnectionString();
    public static readonly string DatabaseName = $"PortalDataTask-{Guid.NewGuid()}";
    private readonly ContextDb _dbContext;
    private bool _disposed;


    public DbFixture()
    {
        var builder = new DbContextOptionsBuilder<ContextDb>();

        builder.UseSqlServer(ConnectionString);

        _dbContext = new ContextDb(builder.Options);
    }

    private void InsertDataTaskActive()
    {
        var dataTask = new Domain.Entities.DataTask(
            DataTaskHelper.IdActive,
            DataTaskHelper.DescriptionActive,
            DataTaskHelper.ValidateDatePendent,
            DataTaskHelper.StatusActive,
            DataTaskHelper.CreatedAtActive,
            null
            );

        dataTask.Id = DataTaskHelper.IdActive;

        _dbContext.DataTasks!.Add(dataTask);
        _dbContext.SaveChanges();
    }
    private void InsertDataTaskPendent()
    {
        var dataTask = new Domain.Entities.DataTask(
            DataTaskHelper.IdPendent,
            DataTaskHelper.DescriptionPendent,
            DataTaskHelper.ValidateDatePendent,
            DataTaskHelper.StatusPendent,
            DataTaskHelper.CreatedAtPendent,
            null
            );

        dataTask.Id = DataTaskHelper.IdPendent;

        _dbContext.DataTasks!.Add(dataTask);
        _dbContext.SaveChanges();
    }

    private void InsertDataTaskInactive()
    {
        var dataTask = new Domain.Entities.DataTask(
            DataTaskHelper.IdInactive,
            DataTaskHelper.DescriptionInactive,
            DataTaskHelper.ValidateDateInactive,
            DataTaskHelper.StatusInactive,
            DataTaskHelper.CreatedAtInactive,
            null
            );

        dataTask.Id = DataTaskHelper.IdInactive;

        _dbContext.DataTasks!.Add(dataTask);
        _dbContext.SaveChanges();
    }

    private static string GetConnectionString()
    {
        var configurationSection = GetTestDataConfiguration();

        var user = configurationSection.GetSection("SqlServer:User").Value;
        var password = configurationSection.GetSection("SqlServer:Password").Value;

        return $"Server=localhost,1433;Database={DatabaseName};User ID={user};Password={password};TrustServerCertificate=True";
    }

    private static IConfiguration GetTestDataConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json")
            .AddEnvironmentVariables()
            .Build();
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed) return;

        if (disposing)
            _dbContext.Database.EnsureDeleted();

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}


[CollectionDefinition("Database")]
public class DatabaseCollection : ICollectionFixture<DbFixture>
{

}
