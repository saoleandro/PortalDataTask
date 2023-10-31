using Moq;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;
using System.Text;
using System.Text.Json;

namespace PortalDataTask.Tests.IntegrationTests.Fixtures;

public class DataTaskControllerFixture : IDisposable
{
    public static StringContent CreateGetDataTasksByFiltersRequest()
    {
        var getDataTask = new GetDataTasksByFiltersRequest
        {
            Description = "description",
            Status = Domain.Enums.StatusEnum.Active,
            ValidateDate = null,
            FinalDate = null
        };

        var body = JsonSerializer.Serialize(getDataTask);
        return new StringContent(body, Encoding.UTF8, "application/json");
    }

    public static StringContent CreateDataTaskBodyBadRequest()
    {
        var createDataTaskRequest = new CreateDataTaskRequest
        {
            Description = "",
            Status = Domain.Enums.StatusEnum.Pendent,
            ValidateDate = new(2025,10,15)
        };

        var body = JsonSerializer.Serialize(createDataTaskRequest);
        return new StringContent(body, Encoding.UTF8, "application/json");
    }

    public static StringContent CreateDataTaskBody()
    {
        var createDataTaskRequest = new CreateDataTaskRequest
        {
            Description = "description",
            Status = Domain.Enums.StatusEnum.Pendent,
            ValidateDate = new(2025, 10, 15)
        };

        var body = JsonSerializer.Serialize(createDataTaskRequest);
        return new StringContent(body, Encoding.UTF8, "application/json");
    }

    public static IDataTaskService CreateIDataTaskServiceMockInternalServerError()
    {
        var iDataTaskServiceMock = new Mock<IDataTaskService>();

        iDataTaskServiceMock
            .Setup(s => s.CreateDataTaskAsync(It.IsAny<CreateDataTaskRequest>()))
            .Throws(new Exception());

        return iDataTaskServiceMock.Object;

    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
