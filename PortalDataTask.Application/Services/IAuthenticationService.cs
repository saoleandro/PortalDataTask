using PortalDataTask.Application.Contracts;

namespace PortalDataTask.Application.Services;

public interface IAuthenticationService
{
    Task<BaseResponse> Login(string login, string password);
}
