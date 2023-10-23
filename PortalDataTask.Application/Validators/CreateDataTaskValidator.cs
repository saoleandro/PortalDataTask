using FluentValidation;
using PortalDataTask.Application.Contracts;

namespace PortalDataTask.Application.Validators;

public class CreateDataTaskValidator : AbstractValidator<CreateDataTaskRequest>
{
    public CreateDataTaskValidator()
    {
        RuleFor(createDataTaskRequest => createDataTaskRequest.Description)
            .NotEmpty()
            .WithMessage(createDataTaskRequest => Resources.AppMessage.Validation_Description);

        RuleFor(createDataTaskRequest => createDataTaskRequest.ValidateDate)
            .NotEmpty()
            .WithMessage(createDataTaskRequest => Resources.AppMessage.Validation_ValidateDate);

        RuleFor(createDataTaskRequest => createDataTaskRequest.Status)
            .NotEmpty()
            .WithMessage(createDataTaskRequest => Resources.AppMessage.Validation_Status);
    }
}
