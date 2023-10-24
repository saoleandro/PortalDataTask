using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Application.Services;
using System.Security.Authentication;

namespace PortalDataTaskApi.Controllers;

[AllowAnonymous]
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService= authenticationService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GeneralErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authenticationService.Login(request.Login, request.Password);
            return Response(response);
        }
        catch (AuthenticationException ae)
        {
            return Unauthorized(new { Error = "Acesso negado !", ae.Message });
        }
        catch(Exception ex)
        {
            var response = new BaseResponse();

            response.AddError(new ErrorResponse
            {
                Message = $"Erro ao autenticar !. Detalhes: {ex.Message}"
            }, System.Net.HttpStatusCode.BadRequest);

            return Response(response);
        }
    }

}
