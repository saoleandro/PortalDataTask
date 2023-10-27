using Azure;
using Moq.AutoMock;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Tests.UnitTests.Helpers;
using PortalDataTaskApi.Controllers;

namespace PortalDataTask.Tests.UnitTests.Fixtures;

public class DataTaskControllerFixture : IDisposable
{
    private readonly DataTaskController _dataTaskController;
    private readonly AutoMocker _autoMocker;

    public DataTaskControllerFixture()
    {
        _autoMocker = new AutoMocker();
        _dataTaskController = _autoMocker.CreateInstance<DataTaskController>();
    }

    public DataTaskController GetDataTaskController() => _dataTaskController;
    public AutoMocker GetAutoMocker() => _autoMocker;

    public static BaseResponse CreateValidCreateDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddData(new CreateDataTaskResponse
        {
            Id = 1
        });

        return baseResponse;
    }

    public static BaseResponse CreateInvalidCreateDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddError(new ErrorResponse
        {
            Message = ConstantsHelper.Anything
        }, System.Net.HttpStatusCode.BadRequest);

        return baseResponse;
    }

    public static BaseResponse CreateValidGetDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddData(new GetDataTaskResponse
        {
            Id = 1,
            Description = ConstantsHelper.Anything,
            Status = Domain.Enums.StatusEnum.Active,
            ValidateDate = new(2023, 10, 10),
            CreatedAt = DateTime.Now,
            UpdatedAt = null
        });

        return baseResponse;
    }

    public static BaseResponse CreateInvalidValidGetDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddError(new ErrorResponse
        {
            Message = ConstantsHelper.Anything
        }, System.Net.HttpStatusCode.BadRequest);

        return baseResponse;
    }

    public static BaseResponse CreateValidUpdateDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddData(new UpdateDataTaskResponse
        {
            Description = ConstantsHelper.Anything,
            Status = Domain.Enums.StatusEnum.Active,
            ValidateDate = new(2023,10,10),
            UpdatedAt= DateTime.Now
        });

        return baseResponse;
    }

    public static BaseResponse CreateInvalidUpdateDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddError(new ErrorResponse
        {
            Message = ConstantsHelper.Anything
        }, System.Net.HttpStatusCode.BadRequest);

        return baseResponse;
    }

    public static BaseResponse CreateValidDeleteDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddData(new DeleteDataTaskResponse
        {
            Id = 1
        });

        return baseResponse;
    }

    public static BaseResponse CreateInvalidDeleteDataTaskResponse()
    {
        var baseResponse = new BaseResponse();
        baseResponse.AddError(new ErrorResponse
        {
            Message = ConstantsHelper.Anything
        }, System.Net.HttpStatusCode.BadRequest);

        return baseResponse;
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
