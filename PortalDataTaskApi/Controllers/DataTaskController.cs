using Microsoft.AspNetCore.Mvc;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;

namespace PortalDataTaskApi.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/dataTasks")]
public class DataTaskController : BaseController
{
    private readonly IDataTaskService _dataTaskService;

    public DataTaskController(IDataTaskService dataTaskService)
    {
        _dataTaskService = dataTaskService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetDataTaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDataTask()
    {
        var response = await _dataTaskService.GetAllDataTask();
        return Response(response);
    }

    [HttpGet("{dataTaskId}")]
    [ProducesResponseType(typeof(GetDataTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDataTaskById([FromRoute] long dataTaskId)
    {
        var response = await _dataTaskService.GetDataTaskById(dataTaskId);
        return Response(response);
    }

    [HttpPost("filters")]
    [ProducesResponseType(typeof(List<GetDataTasksByFiltersResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByFilters([FromBody] GetDataTasksByFiltersRequest request)
    {
        var response = await _dataTaskService.GetByFilters(request);
        return Response(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateDataTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDataTaskAsync([FromBody] CreateDataTaskRequest createDataTaskRequest)
    {
        var response = await _dataTaskService.CreateDataTaskAsync(createDataTaskRequest);
        return Response(response);
    }

    [HttpPut("{dataTaskId}")]
    [ProducesResponseType(typeof(UpdateDataTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDataTaskAsync([FromRoute] long dataTaskId , [FromBody] UpdateDataTaskRequest updateDataTaskRequest)
    {
        var response = await _dataTaskService.UpdateDataTaskAsync(dataTaskId, updateDataTaskRequest);
        return Response(response);
    }

    [HttpDelete("{dataTaskId}")]
    [ProducesResponseType(typeof(CreateDataTaskRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDataTaskAsync([FromRoute] long dataTaskId)
    {
        var response = await _dataTaskService.DeleteDataTaskAsync(dataTaskId);
        return Response(response);
    }

}
