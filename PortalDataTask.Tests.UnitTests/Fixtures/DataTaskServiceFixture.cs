using Moq.AutoMock;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;
using PortalDataTask.Tests.UnitTests.Helpers;

namespace PortalDataTask.Tests.UnitTests.Fixtures;

public class DataTaskServiceFixture : IDisposable
{
    private readonly DataTaskService _dataTaskService;
    private readonly AutoMocker _autoMocker;

    public DataTaskServiceFixture()
    {
        _autoMocker = new AutoMocker();
        _dataTaskService = _autoMocker.CreateInstance<DataTaskService>();
    }

     public DataTaskService GetDataTaskService() => _dataTaskService;
     public AutoMocker GetAutoMocker() => _autoMocker;

    public static GetDataTaskResponse CreateGetDataTaskResponse()
    {
        return new GetDataTaskResponse
        {
            Id = 1,
            Description = ConstantsHelper.Anything,
            CreatedAt = DateTime.UtcNow,
            Status = Domain.Enums.StatusEnum.Active,
            UpdatedAt = null,
            ValidateDate = new(2025, 10, 10)
        };
    }

    public static Domain.Entities.DataTask CreateDataTaskValid()
    {
        return new Domain.Entities.DataTask(
            1,
            ConstantsHelper.Anything,
            new(2025, 10, 10),
            Domain.Enums.StatusEnum.Active,
            DateTime.UtcNow,
            null
            );        
    }

    public static CreateDataTaskRequest CreateValidDataTaskRequest()
    {
        return new CreateDataTaskRequest
        {
            Description = ConstantsHelper.Anything,
            Status = Domain.Enums.StatusEnum.Active,
            ValidateDate = new(2025,10,10)
        };
    }

    public static CreateDataTaskResponse CreateValidDataTaskResponse()
    {
        return new CreateDataTaskResponse
        {
            Id = 1
        };
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}