
namespace PortalDataTask.Application.Contracts;

public class GeneralErrorResponse
{
    public string Error { get; }

    public GeneralErrorResponse(string error)
    {
        Error = error;
    }
}
