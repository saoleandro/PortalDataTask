using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Moq;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;
using PortalDataTask.Domain.Interfaces.Repositories;
using PortalDataTask.Tests.UnitTests.Fixtures;
using PortalDataTask.Tests.UnitTests.Helpers;
using System.Net;

namespace PortalDataTask.Tests.UnitTests.Services;

public class DataTaskServiceTests : IClassFixture<DataTaskServiceFixture>
{
    private readonly DataTaskServiceFixture _fixture;
    private readonly DataTaskService _service;

    public DataTaskServiceTests(DataTaskServiceFixture fixture)
    {
        _fixture = fixture;
        _service = _fixture.GetDataTaskService();
    }

    [Fact]
    public async void Should_Return_DataTask_By_Id_When_Is_Null_Or_Not_Found()
    {
        //Arrange
        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);

        //Act
        var result = await _service.GetDataTaskById(1);

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.Errors.FirstOrDefault()!.Message.Should().Be(Application.Resources.AppMessage.Validation_DataTaskNotFoundById);
        result.Data.Should().BeNull();
    }

    [Fact]
    public async void Should_Return_DataTask_By_Id_When_Valid()
    {
        //Arrange
        var dataTask = DataTaskServiceFixture.CreateDataTaskValid();

        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(dataTask);

        //Act
        var result = await _service.GetDataTaskById(dataTask.Id);

        //Assert
        result.Should().NotBeNull();
        ((GetDataTaskResponse)result.Data).Id.Should().Be(dataTask.Id);
        ((GetDataTaskResponse)result.Data).Description.Should().Be(dataTask.Description);
    }

    [Fact]
    public async Task Should_Not_Update_When_DataTask_Is_Not_Found_By_Id()
    {
        //Arrange
        var updateDataTaskRequest = new UpdateDataTaskRequest
        {
            Description= ConstantsHelper.Anything,
            Status = Domain.Enums.StatusEnum.Pendent,
            ValidateDate = new(2025,10,10)
        };

        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(() => null);

        //Act
        var result = await _service.UpdateDataTaskAsync(1, updateDataTaskRequest);

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.Errors.FirstOrDefault()!.Message.Should().Be(Application.Resources.AppMessage.Validation_DataTaskNotFoundById);
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task Should_Not_Update_When_DataTask_Valid()
    {
        //Arrange
        var dataTask = DataTaskServiceFixture.CreateDataTaskValid();

        var updateDataTaskRequest = new UpdateDataTaskRequest
        {
            Description = ConstantsHelper.Anything,
            Status = Domain.Enums.StatusEnum.Pendent,
            ValidateDate = new(2025, 10, 10)
        };

        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByDescriptionAsync(It.IsAny<string>()))
            .ReturnsAsync(dataTask);

        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(dataTask);

        //Act
        var result = await _service.UpdateDataTaskAsync(1, updateDataTaskRequest);

        //Assert
        result.Data.Should().BeOfType<UpdateDataTaskResponse>();
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        ((UpdateDataTaskResponse)result.Data).Description.Should().Be(updateDataTaskRequest.Description);
        ((UpdateDataTaskResponse)result.Data).Status.Should().Be(updateDataTaskRequest.Status);
        ((UpdateDataTaskResponse)result.Data).ValidateDate.Should().Be(updateDataTaskRequest.ValidateDate);
    }

    [Fact]
    public async Task Should_Not_Add_When_DataTask_Is_Not_Found_By_Id()
    {
        //Arrange
        var createDataRequest = new CreateDataTaskRequest
        {
            Description = ConstantsHelper.Anything,
            ValidateDate = new(2025, 10, 10),
            Status = Domain.Enums.StatusEnum.Pendent
        };

        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByDescriptionAsync(It.IsAny<string>()))
            .ReturnsAsync(DataTaskServiceFixture.CreateDataTaskValid());

        //Act
        var result = await _service.CreateDataTaskAsync(createDataRequest);

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.Errors.FirstOrDefault()!.Message.Should().Be(Application.Resources.AppMessage.Validation_AlreadyDescription);
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task Should_Create_When_DataTask_Valid()
    {
        //Arrange
        var dataTask = DataTaskServiceFixture.CreateDataTaskValid();

        var createDataRequest = new CreateDataTaskRequest
        {
            Description = ConstantsHelper.Anything,
            ValidateDate = new(2025, 10, 10),
            Status = Domain.Enums.StatusEnum.Pendent
        };

        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(dataTask);

        //Act
        var result = await _service.CreateDataTaskAsync(createDataRequest);

        //Assert
        result.Data.Should().BeOfType<CreateDataTaskResponse>();
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_Not_Delete_When_DataTask_Is_Not_Found_By_Id()
    {
        //Arrange
        _fixture.GetAutoMocker().GetMock<IDataTaskRepository>()
            .Setup(f => f.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync(() => null);

        //Act
        var result = await _service.DeleteDataTaskAsync(It.IsAny<long>());

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.Errors.FirstOrDefault()!.Message.Should().Be(Application.Resources.AppMessage.Validation_DataTaskNotFoundById);
        result.Data.Should().BeNull();
    }
}
