using FluentAssertions;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Tests.IntegrationTests.Extensions;
using PortalDataTask.Tests.IntegrationTests.Fixtures;
using Xunit;

namespace PortalDataTask.Tests.IntegrationTests.Controllers;

public class DataTaskControllerIntegrationTests : IClassFixture<DataTaskApiApplicationFactory>,
                                                        IClassFixture<DataTaskControllerFixture>
{
    private readonly DataTaskApiApplicationFactory _applicationFactory;
    private readonly DataTaskControllerFixture _fixture;

    public DataTaskControllerIntegrationTests(DataTaskApiApplicationFactory applicationFactory, 
                                              DataTaskControllerFixture fixture)
    {
        _applicationFactory = applicationFactory;
        _fixture = fixture;
    }

    [Fact]
    public async Task Create_DataTask_Should_Return_Created()
    {
        //Arrange 
        var client = _applicationFactory.CreateClient();

        //Act
        var result = await client.PostAsync("api/v1.0/dataTasks",
            DataTaskControllerFixture.CreateDataTaskBodyBadRequest());

        //Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var createUserResponse = await result.ParseResponseToObject<CreateDataTaskResponse>();
        createUserResponse.Should().NotBeNull();
    }
}
