using Microsoft.AspNetCore.Mvc;
using PortalDataTask.Application.Contracts;

namespace PortalDataTaskApi.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected new virtual IActionResult Response<T>(BaseResponse<T> result)
    {
        object? value = result.Errors.Any() ? result.Errors : ((object?)result.Data);
        return StatusCode(result.StatusCode, value);
    }

    protected new virtual IActionResult Response(BaseResponse result)
    {
        return Response((BaseResponse<object>)result);
    }
}
