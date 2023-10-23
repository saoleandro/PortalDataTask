using PortalDataTask.Application.Contracts;

namespace PortalDataTask.Application.Services;

public interface IDataTaskService
{
    Task<BaseResponse> GetAllDataTask();
    Task<BaseResponse> GetDataTaskById(long id);
    Task<BaseResponse> GetByFilters(GetDataTasksByFiltersRequest request);
    Task<BaseResponse> CreateDataTaskAsync(CreateDataTaskRequest createDataTaskRequest);
    Task<BaseResponse> UpdateDataTaskAsync(long id, UpdateDataTaskRequest updateDataTaskRequest);
}
