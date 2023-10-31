using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PortalDataTask.Domain.Interfaces.Repositories;
using PortalDataTask.Tests.IntegrationTests.Helpers;
using Xunit;

namespace PortalDataTask.Tests.IntegrationTests.Repositories;

[Collection("Database")]
public class DataTaskRepositoryIntegrationTests : IClassFixture<DataTaskApiApplicationFactory>
{
    private readonly DataTaskApiApplicationFactory _applicationFactory;

    public DataTaskRepositoryIntegrationTests(DataTaskApiApplicationFactory applicationFactory)
    {
        _applicationFactory = applicationFactory;
    }

    [Fact]
    public async void Should_Get_By_DatTask_By_Id()
    {
        //Arrange
        var dataTaskRepository = _applicationFactory.Services.GetRequiredService<IDataTaskRepository>();

        //Act
        var dataTask = await dataTaskRepository.GetByIdAsync(DataTaskHelper.IdInactive);

        //Assert
        dataTask!.Id.Should().Be(DataTaskHelper.IdInactive);
        dataTask.Description.Should().Be(DataTaskHelper.DescriptionInactive);
        dataTask.ValidateDate.Should().Be(DataTaskHelper.ValidateDateInactive);
        dataTask.Status.Should().Be(DataTaskHelper.StatusInactive);
    }

    [Fact]
    public async void Should_Get_By_DatTask_By_Description()
    {
        //Arrange
        var dataTaskRepository = _applicationFactory.Services.GetRequiredService<IDataTaskRepository>();

        //Act
        var dataTask = await dataTaskRepository.GetByDescriptionAsync(DataTaskHelper.DescriptionPendent);

        //Assert
        dataTask!.Id.Should().Be(DataTaskHelper.IdPendent);
        dataTask.Description.Should().Be(DataTaskHelper.DescriptionPendent);
        dataTask.ValidateDate.Should().Be(DataTaskHelper.ValidateDatePendent);
        dataTask.Status.Should().Be(DataTaskHelper.StatusPendent);
    }
}
