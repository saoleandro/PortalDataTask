using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;
using PortalDataTask.Tests.UnitTests.Fixtures;
using PortalDataTask.Tests.UnitTests.Helpers;
using PortalDataTaskApi.Controllers;

namespace PortalDataTask.Tests.UnitTests.Controllers;

public class DataTaskControllerUnitTests : IClassFixture<DataTaskControllerFixture>
{
    private readonly DataTaskControllerFixture _fixture;
    private readonly DataTaskController _dataTaskController;

    public DataTaskControllerUnitTests(DataTaskControllerFixture dataTaskControllerFixture)
    {
        _fixture = dataTaskControllerFixture;
        _dataTaskController = _fixture.GetDataTaskController();
    }

    [Fact]
    public async void Should_Create_DataTask_And_Return_Ok()
    {
        //Arrange
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(x => x.CreateDataTaskAsync(It.IsAny<CreateDataTaskRequest>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateValidCreateDataTaskResponse);

        //Act
        var result = await _dataTaskController.CreateDataTaskAsync(new CreateDataTaskRequest());

        //Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var createDataTaskResponse = objectResult.Value.Should().BeOfType<CreateDataTaskResponse>().Subject;
        createDataTaskResponse.Id.ToString().Should().Be("1");
    }

    [Fact]
    public async void Should_Not_Create__DataTask_And_Return_BadRequest()
    {
        //Arranque
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(x => x.CreateDataTaskAsync(It.IsAny<CreateDataTaskRequest>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateInvalidCreateDataTaskResponse());

        //Act
        var result = await _dataTaskController.CreateDataTaskAsync(new CreateDataTaskRequest());

        //Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        var badRequestResponse = objectResult.Value.Should().BeOfType<List<ErrorResponse>>().Subject;
        badRequestResponse.Should().Contain(x => x.Message == ConstantsHelper.Anything);
    }

    [Fact]
    public async void Should_Receive_DataTask_Data_For_GetDataTaskById_And_Returns_Ok()
    {
        //Fixture
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(f => f.GetDataTaskById(It.IsAny<long>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateValidGetDataTaskResponse());

        //Act
        var result = await _dataTaskController.GetDataTaskById(It.IsAny<long>());

        //Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async void Should_Receive_Invalid_Id_For_GetUserById_And_Returns_BadRequest()
    {
        //Fixture
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(f => f.GetDataTaskById(It.IsAny<long>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateInvalidValidGetDataTaskResponse());

        //Act
        var result = await _dataTaskController.GetDataTaskById(It.IsAny<long>());

        //Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async void Should_Update_DataTask_And_Return_Ok()
    {
        //Arrange
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(x => x.UpdateDataTaskAsync(It.IsAny<long>(),It.IsAny<UpdateDataTaskRequest>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateValidUpdateDataTaskResponse);

        //Act
        var result = await _dataTaskController.UpdateDataTaskAsync(1,new UpdateDataTaskRequest());

        //Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var updateDataTaskResponse = objectResult.Value.Should().BeOfType<UpdateDataTaskResponse>().Subject;
        updateDataTaskResponse?.Description!.ToString().Should().Be(ConstantsHelper.Anything);
    }

    [Fact]
    public async void Should_Not_Update_DataTask_And_Return_BadRequest()
    {
        //Arranque
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(x => x.UpdateDataTaskAsync(It.IsAny<long>(),It.IsAny<UpdateDataTaskRequest>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateInvalidUpdateDataTaskResponse());

        //Act
        var result = await _dataTaskController.UpdateDataTaskAsync(1, new UpdateDataTaskRequest());

        //Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        var badRequestResponse = objectResult.Value.Should().BeOfType<List<ErrorResponse>>().Subject;
        badRequestResponse.Should().Contain(x => x.Message == ConstantsHelper.Anything);
    }

    [Fact]
    public async void Should_Delete_DataTask_And_Return_Ok()
    {
        //Arrange
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(x => x.DeleteDataTaskAsync(It.IsAny<long>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateValidDeleteDataTaskResponse);

        //Act
        var result = await _dataTaskController.DeleteDataTaskAsync(1);

        //Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        var deleteDataTaskResponse = objectResult.Value.Should().BeOfType<DeleteDataTaskResponse>().Subject;
        deleteDataTaskResponse?.Id!.ToString().Should().Be("1");
    }

    [Fact]
    public async void Should_Not_Delete_DataTask_And_Return_BadRequest()
    {
        //Arranque
        _fixture.GetAutoMocker().GetMock<IDataTaskService>()
            .Setup(x => x.DeleteDataTaskAsync(It.IsAny<long>()))
            .ReturnsAsync(DataTaskControllerFixture.CreateInvalidDeleteDataTaskResponse);

        //Act
        var result = await _dataTaskController.DeleteDataTaskAsync(1);

        //Assert
        var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
        objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        var badRequestResponse = objectResult.Value.Should().BeOfType<List<ErrorResponse>>().Subject;
        badRequestResponse.Should().Contain(x => x.Message == ConstantsHelper.Anything);
    }

}


        