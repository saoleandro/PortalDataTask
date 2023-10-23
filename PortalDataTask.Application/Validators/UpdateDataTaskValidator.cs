using FluentValidation;
using PortalDataTask.Application.Contracts;

namespace PortalDataTask.Application.Validators;

public class UpdateDataTaskValidator : AbstractValidator<UpdateDataTaskRequest>
{
	public UpdateDataTaskValidator()
	{
		When(updateDataTaskRequest => !String.IsNullOrWhiteSpace(updateDataTaskRequest.Description), () =>
		{
            RuleFor(createDataTaskRequest => createDataTaskRequest.Description)
            .NotEmpty()
            .WithMessage(createDataTaskRequest => Resources.AppMessage.Validation_Description);
        });

        When(updateDataTaskRequest => updateDataTaskRequest.ValidateDate != (DateTime?)null, () =>
        {
            RuleFor(createDataTaskRequest => createDataTaskRequest.ValidateDate)
            .NotEmpty()
            .WithMessage(createDataTaskRequest => Resources.AppMessage.Validation_ValidateDate);
        });

        When(updateDataTaskRequest => updateDataTaskRequest.Status.HasValue, () =>
        {
            RuleFor(createDataTaskRequest => createDataTaskRequest.Status)
            .NotEmpty()
            .WithMessage(createDataTaskRequest => Resources.AppMessage.Validation_Status);
        });
    }
}
