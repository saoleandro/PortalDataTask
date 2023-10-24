using Microsoft.AspNetCore.Components.Forms;
using PortalDataTask.Application.Contracts;
using PortalDataTask.Domain.Interfaces.Repositories;
using PortalDataTask.Infra.CrossCutting.Services.Extensions;

namespace PortalDataTask.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Login(string login, string password)
    {
        var response = new BaseResponse();

        var user = await _userRepository.GetAsync(login);
        var pass = PasswordEncryption.GetSHA1(password);

        if (user == null || PasswordEncryption.GetSHA1(password) != user.Password)
        {
            response.AddError(new ErrorResponse
            {
                Message = "Usuário ou Senha inválida"
            }, System.Net.HttpStatusCode.BadRequest);

        }
        else
        {
            response.AddData(new LoginResponse
            {
                User = user.Login,
                Token = pass
            });
        }

        return response;
    }
}
